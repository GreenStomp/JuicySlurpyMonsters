using UnityEngine;
using System.Collections.Generic;
public sealed class CoinManager : MonoBehaviour
{
    //public static CoinManager Instance { get; private set; }

    //public Coin CoinPrefab;

    //private Dictionary<Platform, Queue<Coin>> coins = new Dictionary<Platform, Queue<Coin>>();

    //void Awake()
    //{
    //    //Singleton
    //    if (Instance != null)
    //        Destroy(this);
    //    else
    //    {
    //        Instance = this;
    //        PoolManager.InitializePool<Coin>(CoinPrefab, 50);
    //    }
    //}

    //public void OnPlatformCreated(Platform plat)
    //{
    //    Transform[] interestPoints = plat.GetCoinSpawnPoints();
    //    if (interestPoints == null || interestPoints.Length <= 1)
    //        return;

    //    Transform firstPoint = interestPoints[0];
    //    Queue<Coin> spawnedCoins = new Queue<Coin>(/* n of coins to spawn in total*/);
    //    for (int i = 1; i < interestPoints.Length; i++)
    //    {
    //        Transform secondPoint = interestPoints[i];

    //        //Spawno tot coin fra firstPoint e second Point
    //        //for (int i = 0; i < length; i++)
    //        //{

    //        //per ogni spawned coin fare questo
    //        Coin spawned = PoolManager.Get(CoinPrefab);
    //        spawnedCoins.Enqueue(spawned);

    //        //}

    //        firstPoint = secondPoint;
    //    }
    //    coins.Add(plat, spawnedCoins);
    //}
    //public void OnPlatformDestroyed(Platform plat)
    //{
    //    if(coins.ContainsKey(plat))
    //    {
    //        Queue<Coin> elementsToPool = coins[plat];
    //        while (elementsToPool.Count != 0)
    //        {
    //            PoolManager.Recycle(elementsToPool.Dequeue());
    //        }
    //    }
    //}

    //void OnDestroy()
    //{
    //    if (Instance == this)
    //        Instance = null;
    //}
}