using UnityEngine;
using System;
using System.Collections.Generic;
public sealed class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }

    public const int MinWaypointNumber = 2;
    public const int MinCoinsPerSegmentNumber = 0;
    public const int MinCoinsInSceneNumber = 0;
    public const int MaxCoinsPerSegmentNumber = int.MaxValue;
    public const int MaxCoinsInSceneNumber = int.MaxValue;

    /// <summary>
    /// Approximate number of coins currently in the scene. Number may be slightly in excess
    /// </summary>
    public int ApproximateNumberOfCoinsInScene { get { return coins.Count; } }
    /// <summary>
    /// Base coin spawn number for each segment
    /// </summary>
    public int CoinsPerSegment { get { return coinsPerSegment; } set { coinsPerSegment = Mathf.Clamp(value, MinCoinsPerSegmentNumber, MaxCoinsPerSegmentNumber); } }
    /// <summary>
    /// Max number of coins in the scene. The more coins are currently in the scene the more heavy will be the operation of changing this value
    /// </summary>
    public int MaxCoinsInScene
    {
        get { return maxCoinsInScene; }
        set
        {
            //mi salvo il valore clampato del value
            int res = Mathf.Clamp(value, MinCoinsInSceneNumber, MaxCoinsInSceneNumber);

            //se il res è diverso dal valore di maxCoins corrente allora fa i calcoli
            if (res != maxCoinsInScene)
            {
                maxCoinsInScene = res;

                //creo la nuova coda con tot elementi
                Queue<Coin> temp = new Queue<Coin>(maxCoinsInScene);

                //sposto le coin dalla coda precedente alla nuova fino a che non terminano o non c'è più spazio nella nuova coda (per evitare l'allargamento della nuova coda)
                while (temp.Count < maxCoinsInScene)
                {
                    if (coins.Count == 0)
                        break;
                    temp.Enqueue(coins.Dequeue());
                }

                //se sono rimaste delle coin nella vecchia coda le elimino
                while (coins.Count != 0)
                {
                    PoolManager.Recycle(coins.Dequeue());
                }

                //setto la nuova coda come standard
                coins = temp;
            }
        }
    }
    /// <summary>
    /// Total number of missed coins
    /// </summary>
    public uint MissedCoins { get { return missedCoins; } }
    /// <summary>
    /// Total number of coins picked up
    /// </summary>
    public uint PickedCoins { get { return pickedCoins; } }

    [SerializeField]
    private Coin coinPrefab;
    [SerializeField]
    private int coinsPerSegment = 10;
    [SerializeField]
    private int maxCoinsInScene = 200;

    private uint missedCoins;
    private uint pickedCoins;

    private Queue<Coin> coins;
    private List<CoinSpawnerWaypoint> interestPoints;
    /// <summary>
    /// Event called from PlatformManager whenever a platform is created
    /// </summary>
    /// <param name="plat">created platform</param>
    public void OnPlatformCreated(Platform plat)
    {
        //mi prendo il parent di tutti i pickable della platform
        Transform coinParent = plat.PickableHolder;

        //non fare niente se pickable holder non esiste
        if (coinParent == null)
            return;

        //mi prendo tutti i waypoint per i coin
        coinParent.GetComponentsInChildren<CoinSpawnerWaypoint>(false, interestPoints);

        int length = interestPoints.Count;

        //se non ci sono abbastanza waypoint non fare niente
        if (length < MinWaypointNumber)
        {
            interestPoints.Clear();
            return;
        }

        //mi prendo il primo point
        CoinSpawnerWaypoint current = interestPoints[0];
        Transform firstPoint = current.transform;


        for (int i = 1; i < length; i++)
        {
            //Mi salvo il numero di coin da spawnare, se il waypoint è settato per overridare il coinManager uso il suo count
            int coinsToSpawn = current.IsOverriding ? current.CoinsThisSegment : coinsPerSegment;

            //mi prendo il secondo point
            current = interestPoints[i];
            Transform secondPoint = current.transform;

            //Gestisco il caso in cui le coin superino il numero massimo consentito
            if (coinsToSpawn + coins.Count > maxCoinsInScene)
                coinsToSpawn = maxCoinsInScene - coins.Count;
            if (coinsToSpawn == 0)
            {
                //setto il primo point al secondo per il prossimo ciclo
                firstPoint = secondPoint;
                continue;
            }

            //posizione e rotazione iniziali
            Vector3 currentPos = firstPoint.position;
            Quaternion currentRot = firstPoint.rotation;

            //direzione e distanza fra inizio e fine
            Vector3 dir = secondPoint.position - currentPos;
            float magnitude = dir.magnitude;
            dir.Normalize();

            //intervallo che indica ogni quanto piazzare una coin
            float interval = magnitude / coinsToSpawn;

            //Spawno tot coin fra firstPoint e second Point ad intervalli regolari
            for (int j = 0; j < coinsToSpawn; j++, currentPos += dir * interval)
            {
                //per ogni spawned coin faccio il get dal pool, mi prendo la sua transform e setto posizione , rotazione e parent.
                Coin spawned = PoolManager.Get(coinPrefab);
                coins.Enqueue(spawned);
                Transform coinT = spawned.transform;
                //coinT.parent = coinParent;
                coinT.SetPositionAndRotation(currentPos, currentRot);
            }

            //setto il primo point al secondo per il prossimo ciclo
            firstPoint = secondPoint;
        }

        interestPoints.Clear();
    }
    /// <summary>
    /// Resets the status of all coins in the scene, Recycling them
    /// </summary>
    /// <param name="resetCounters">determines if missedCoin and pickedCoin counters shall be resetted</param>
    public void ResetCoins(bool resetCounters = true)
    {
        while (coins.Count != 0)
        {
            Coin c = coins.Dequeue();
            if (c != null)
                PoolManager.Recycle(c);
        }

        if (resetCounters)
        {
            pickedCoins = 0;
            missedCoins = 0;
        }
    }
    void Awake()
    {
        //Singleton
        if (Instance != null)
            Destroy(this);
        else
        {
            if (coinPrefab == null)
                throw new NullReferenceException("Coin prefab has not been setted in the CoinManager");
            Instance = this;
            coins = new Queue<Coin>(maxCoinsInScene);
            PoolManager.InitializePool<Coin>(coinPrefab, maxCoinsInScene);
            interestPoints = new List<CoinSpawnerWaypoint>();
        }
    }
    void Update()
    {
        //mi prendo la prima coin della coda e il deathPlane
        Coin c = coins.Count == 0 ? null : coins.Peek();
        PlatDeathPlane p = PlatformManager.Instance.DeathPlane;

        //riciclo il primo elemento della coda fintanto che trovo elementi validi nella coda e che essi siano stati superati dal deathPlane 
        while (c != null && p.DeathPlane.SameSide(p.DeathSidePoint, c.Position))
        {
            //aggiorno i counter
            if (c.gameObject.activeSelf)
                missedCoins++;
            else
                pickedCoins++;

            //riciclo la coin superata dal deathPlane
            PoolManager.Recycle(coins.Dequeue());

            //prendo il nuovo primo elemento dalla coda
            c = coins.Count == 0 ? null : coins.Peek();
        }
    }
    void OnDestroy()
    {
        if (Instance == this)
        {
            ResetCoins();
            Instance = null;
        }
    }
    void OnValidate()
    {
        CoinsPerSegment = coinsPerSegment;
        MaxCoinsInScene = maxCoinsInScene;
    }
}