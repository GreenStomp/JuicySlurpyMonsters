using UnityEngine;
using System.Collections.Generic;
using SOPRO;
/// <summary>
/// Class that holdes Coin pools and spawn logic
/// </summary>
[CreateAssetMenu(fileName = "CoinManager", menuName = "Level/Pickables/Coin/Manager")]
public class CoinManager : ScriptableObject
{
    /// <summary>
    /// Pools used to spawn coins
    /// </summary>
    public SOPool[] CoinPools = new SOPool[0];

    /// <summary>
    /// Spawns a set of coins
    /// </summary>
    /// <param name="positions">positions to use to spawn coins</param>
    /// <param name="maxCount">max number of coins to spawn</param>
    /// <param name="coinCount">effective number of coins spawned</param>
    /// <param name="parent">parent obj for spawned coins</param>
    /// <param name="poolIndex">pool index used. If negative index will be chosen randomly</param>
    public void SpawnCoins(List<Vector3> positions, int maxCount, out int coinCount, Transform parent = null, int poolIndex = -1)
    {
        coinCount = 0;
        if (CoinPools.Length == 0)
            return;

        int index = poolIndex < 0 ? Random.Range(0, CoinPools.Length - 1) : poolIndex;

        SOPool pool = CoinPools[index];

        coinCount = Mathf.Min(positions.Count, maxCount);

        for (int i = 0; i < coinCount; i++)
        {
            int nullObjsRemovedFromPool;
            GameObject go = pool.Get(parent, out nullObjsRemovedFromPool);
            Coin c = go.GetComponentInChildren<Coin>();
            c.Pool = pool;
            go.transform.position = positions[i];
        }
    }
    /// <summary>
    /// Spawns a set of coins
    /// </summary>
    /// <param name="positions">positions to use to spawn coins</param>
    /// <param name="maxCount">max number of coins to spawn</param>
    /// <param name="spawnedCoins">array filled with spawned coins</param>
    /// <param name="spawnedCoinsStartIndex">array start index</param>
    /// <param name="coinCount">effective number of coins spawned</param>
    /// <param name="parent">parent obj for spawned coins</param>
    /// <param name="poolIndex">pool index used. If negative index will be chosen randomly</param>
    public void SpawnCoins(List<Vector3> positions, int maxCount, Coin[] spawnedCoins, uint spawnedCoinsStartIndex, out int coinCount, Transform parent = null, int poolIndex = -1)
    {
        coinCount = 0;
        if (CoinPools.Length == 0 || spawnedCoins == null)
            return;

        int index = poolIndex < 0 ? Random.Range(0, CoinPools.Length - 1) : poolIndex;

        SOPool pool = CoinPools[index];

        coinCount = Mathf.Min(Mathf.Min(positions.Count, spawnedCoins.Length - (int)spawnedCoinsStartIndex), maxCount);

        for (int i = 0; i < coinCount; i++)
        {
            int nullObjsRemovedFromPool;
            GameObject go = pool.Get(parent, out nullObjsRemovedFromPool);
            Coin c = go.GetComponentInChildren<Coin>();
            c.Pool = pool;
            go.transform.position = positions[i];
            spawnedCoins[i + (int)spawnedCoinsStartIndex] = c;
        }
    }
    /// <summary>
    /// Spawns a set of coins. First iteration can be used to get total number of coins that wil be spawned
    /// </summary>
    /// <param name="positions">positions to use to spawn coins</param>
    /// <param name="maxCount">max number of coins to spawn</param>
    /// <param name="spawnsPerFrame">Number of coins to spawn per frame</param>
    /// <param name="parent">parent obj for spawned coins</param>
    /// <param name="poolIndex">pool index used. If negative index will be chosen randomly</param>
    /// <returns>enumerator holding the total number of coins that will be spawned</returns>
    public IEnumerator<int> SpawnCoinsCoroutine(List<Vector3> positions, int maxCount, int spawnsPerFrame, Transform parent = null, int poolIndex = -1)
    {
        if (CoinPools.Length == 0 || spawnsPerFrame <= 0)
            yield break;

        int index = poolIndex < 0 ? Random.Range(0, CoinPools.Length - 1) : poolIndex;

        SOPool pool = CoinPools[index];

        int j = spawnsPerFrame;

        int length = Mathf.Min(positions.Count, maxCount);

        for (int i = 0; i < length; i++, j--)
        {
            if (j <= 0)
            {
                j = spawnsPerFrame;
                yield return length;
            }

            int nullObjsRemovedFromPool;
            GameObject go = pool.Get(parent, out nullObjsRemovedFromPool);
            Coin c = go.GetComponentInChildren<Coin>();
            c.Pool = pool;
            go.transform.position = positions[i];
        }
    }
    /// <summary>
    /// Spawns a set of coins. First iteration can be used to get total number of coins that wil be spawned
    /// </summary>
    /// <param name="positions">positions to use to spawn coins</param>
    /// <param name="maxCount">max number of coins to spawn</param>
    /// <param name="spawnsPerFrame">Number of coins to spawn per frame</param>
    /// <param name="spawnedCoins">array filled with spawned coins</param>
    /// <param name="spawnedCoinsStartIndex">array start index</param>
    /// <param name="parent">parent obj for spawned coins</param>
    /// <param name="poolIndex">pool index used. If negative index will be chosen randomly</param>
    /// <returns>enumerator holding the total number of coins that will be spawned</returns>
    public IEnumerator<int> SpawnCoinsCoroutine(List<Vector3> positions, int maxCount, int spawnsPerFrame, Coin[] spawnedCoins, uint spawnedCoinsStartIndex, Transform parent = null, int poolIndex = -1)
    {
        if (CoinPools.Length == 0 || spawnsPerFrame <= 0 || spawnedCoins == null)
            yield break;

        int index = poolIndex < 0 ? Random.Range(0, CoinPools.Length - 1) : poolIndex;

        SOPool pool = CoinPools[index];

        int j = spawnsPerFrame;

        int length = Mathf.Min(Mathf.Min(positions.Count, spawnedCoins.Length - (int)spawnedCoinsStartIndex), maxCount);

        for (int i = 0; i < length; i++, j--)
        {
            if (j <= 0)
            {
                j = spawnsPerFrame;
                yield return length;
            }

            int nullObjsRemovedFromPool;
            GameObject go = pool.Get(parent, out nullObjsRemovedFromPool);
            Coin c = go.GetComponentInChildren<Coin>();
            c.Pool = pool;
            go.transform.position = positions[i];
            spawnedCoins[i + (int)spawnedCoinsStartIndex] = c;
        }
    }
    /// <summary>
    /// Spawns a set of coins
    /// </summary>
    /// <param name="positions">positions to use to spawn coins</param>
    /// <param name="maxCount">max number of coins to spawn</param>
    /// <param name="coinCount">effective number of coins spawned</param>
    /// <param name="parent">parent obj for spawned coins</param>
    /// <param name="poolIndex">pool index used. If negative index will be chosen randomly</param>
    public void SpawnCoins(Vector3[] positions, int maxCount, out int coinCount, Transform parent = null, int poolIndex = -1)
    {
        coinCount = 0;
        if (CoinPools.Length == 0)
            return;

        int index = poolIndex < 0 ? Random.Range(0, CoinPools.Length - 1) : poolIndex;

        SOPool pool = CoinPools[index];

        coinCount = Mathf.Min(positions.Length, maxCount);

        for (int i = 0; i < coinCount; i++)
        {
            int nullObjsRemovedFromPool;
            GameObject go = pool.Get(parent, out nullObjsRemovedFromPool);
            Coin c = go.GetComponentInChildren<Coin>();
            c.Pool = pool;
            go.transform.position = positions[i];
        }
    }
    /// <summary>
    /// Spawns a set of coins
    /// </summary>
    /// <param name="positions">positions to use to spawn coins</param>
    /// <param name="maxCount">max number of coins to spawn</param>
    /// <param name="spawnedCoins">array filled with spawned coins</param>
    /// <param name="spawnedCoinsStartIndex">array start index</param>
    /// <param name="coinCount">effective number of coins spawned</param>
    /// <param name="parent">parent obj for spawned coins</param>
    /// <param name="poolIndex">pool index used. If negative index will be chosen randomly</param>
    public void SpawnCoins(Vector3[] positions, int maxCount, Coin[] spawnedCoins, uint spawnedCoinsStartIndex, out int coinCount, Transform parent = null, int poolIndex = -1)
    {
        coinCount = 0;
        if (CoinPools.Length == 0 || spawnedCoins == null)
            return;

        int index = poolIndex < 0 ? Random.Range(0, CoinPools.Length - 1) : poolIndex;

        SOPool pool = CoinPools[index];

        coinCount = Mathf.Min(Mathf.Min(positions.Length, spawnedCoins.Length - (int)spawnedCoinsStartIndex), maxCount);

        for (int i = 0; i < coinCount; i++)
        {
            int nullObjsRemovedFromPool;
            GameObject go = pool.Get(parent, out nullObjsRemovedFromPool);
            Coin c = go.GetComponentInChildren<Coin>();
            c.Pool = pool;
            go.transform.position = positions[i];
            spawnedCoins[i + (int)spawnedCoinsStartIndex] = c;
        }
    }
    /// <summary>
    /// Spawns a set of coins. First iteration can be used to get total number of coins that wil be spawned
    /// </summary>
    /// <param name="positions">positions to use to spawn coins</param>
    /// <param name="maxCount">max number of coins to spawn</param>
    /// <param name="spawnsPerFrame">Number of coins to spawn per frame</param>
    /// <param name="parent">parent obj for spawned coins</param>
    /// <param name="poolIndex">pool index used. If negative index will be chosen randomly</param>
    /// <returns>enumerator holding the total number of coins that will be spawned</returns>
    public IEnumerator<int> SpawnCoinsCoroutine(Vector3[] positions, int maxCount, int spawnsPerFrame, Transform parent = null, int poolIndex = -1)
    {
        if (CoinPools.Length == 0 || spawnsPerFrame <= 0)
            yield break;

        int index = poolIndex < 0 ? Random.Range(0, CoinPools.Length - 1) : poolIndex;

        SOPool pool = CoinPools[index];

        int j = spawnsPerFrame;

        int length = Mathf.Min(positions.Length, maxCount);

        for (int i = 0; i < length; i++, j--)
        {
            if (j <= 0)
            {
                j = spawnsPerFrame;
                yield return length;
            }

            int nullObjsRemovedFromPool;
            GameObject go = pool.Get(parent, out nullObjsRemovedFromPool);
            Coin c = go.GetComponentInChildren<Coin>();
            c.Pool = pool;
            go.transform.position = positions[i];
        }
    }
    /// <summary>
    /// Spawns a set of coins. First iteration can be used to get total number of coins that wil be spawned
    /// </summary>
    /// <param name="positions">positions to use to spawn coins</param>
    /// <param name="maxCount">max number of coins to spawn</param>
    /// <param name="spawnsPerFrame">Number of coins to spawn per frame</param>
    /// <param name="spawnedCoins">array filled with spawned coins</param>
    /// <param name="spawnedCoinsStartIndex">array start index</param>
    /// <param name="parent">parent obj for spawned coins</param>
    /// <param name="poolIndex">pool index used. If negative index will be chosen randomly</param>
    /// <returns>enumerator holding the total number of coins that will be spawned</returns>
    public IEnumerator<int> SpawnCoinsCoroutine(Vector3[] positions, int maxCount, int spawnsPerFrame, Coin[] spawnedCoins, uint spawnedCoinsStartIndex, Transform parent = null, int poolIndex = -1)
    {
        if (CoinPools.Length == 0 || spawnsPerFrame <= 0 || spawnedCoins == null)
            yield break;

        int index = poolIndex < 0 ? Random.Range(0, CoinPools.Length - 1) : poolIndex;

        SOPool pool = CoinPools[index];

        int j = spawnsPerFrame;

        int length = Mathf.Min(Mathf.Min(positions.Length, spawnedCoins.Length - (int)spawnedCoinsStartIndex), maxCount);

        for (int i = 0; i < length; i++, j--)
        {
            if (j <= 0)
            {
                j = spawnsPerFrame;
                yield return length;
            }

            int nullObjsRemovedFromPool;
            GameObject go = pool.Get(parent, out nullObjsRemovedFromPool);
            Coin c = go.GetComponentInChildren<Coin>();
            c.Pool = pool;
            go.transform.position = positions[i];
            spawnedCoins[i + (int)spawnedCoinsStartIndex] = c;
        }
    }
}