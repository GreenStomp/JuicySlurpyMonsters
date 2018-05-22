using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class that manages Several Pool-GameObject- by using the IPoolable interface
/// </summary>
public static class PoolManager
{
    private static readonly Func<GameObject, GameObject> allocator;
    private static readonly Action<GameObject> onPreallocation;
    private static readonly Action<GameObject> onDestroy;
    private static Dictionary<GameObject, Pool<GameObject>> pools;
    static PoolManager()
    {
        pools = new Dictionary<GameObject, Pool<GameObject>>();
        allocator = (go) => GameObject.Instantiate<GameObject>(go);
        onDestroy = (go) => GameObject.Destroy(go);
        onPreallocation = (go) => go.SetActive(false);
    }
    /// <summary>
    /// Initialize a pool of the given ipoolable if not already initialized
    /// </summary>
    /// <typeparam name="T">IPoolable type</typeparam>
    /// <param name="original">original instance</param>
    public static void InitializePool<T>(T original) where T : IPoolable
    {
        if (!pools.ContainsKey(original.Self))
        {
            original.Prefab = original;
            pools.Add(original.Self, new Pool<GameObject>(allocator));
        }
    }
    /// <summary>
    /// Initialize a pool of the given ipoolable if not already initialized
    /// </summary>
    /// <typeparam name="T">IPoolable type</typeparam>
    /// <param name="original">original instance</param>
    /// <param name="preallocation">number of elements the pool must start from</param>
    public static void InitializePool<T>(T original, int preallocation) where T : IPoolable
    {
        if (!pools.ContainsKey(original.Self))
        {
            original.Prefab = original;
            pools.Add(original.Self, new Pool<GameObject>(allocator, original.Self, preallocation, onPreallocation));
        }
    }
    /// <summary>
    /// Gets the total number of elements stored in a pool
    /// </summary>
    /// <typeparam name="T">ipoolable type</typeparam>
    /// <param name="original">instance</param>
    /// <returns>number of elements stored in the pool</returns>
    public static int GetElementsStoredCount<T>(T original) where T : IPoolable
    {
        return pools[original.Prefab.Self].ElementsStored;
    }
    /// <summary>
    /// Gets the overall total number of elements stored in all managed pools
    /// </summary>
    /// <returns>number of elements stored in all managed pools</returns>
    public static int GetTotalElementsStoredCount()
    {
        int stored = 0;
        foreach (Pool<GameObject> item in pools.Values)
            stored += item.ElementsStored;
        return stored;
    }
    /// <summary>
    /// Deactivates and recycles the given instance
    /// </summary>
    /// <typeparam name="T">ipoolable type</typeparam>
    /// <param name="toRecycle">instance to recycle</param>
    public static void Recycle<T>(T toRecycle) where T : IPoolable
    {
        toRecycle.Self.SetActive(false);
        pools[toRecycle.Prefab.Self].Recycle(toRecycle.Self);
    }
    /// <summary>
    /// Deactivates and recycles the given instance
    /// </summary>
    /// <typeparam name="T">ipoolable type</typeparam>
    /// <param name="toRecycle">instance to recycle</param>
    /// <param name="onRecycle">action called on the recycled element before deactivation</param>
    public static void Recycle<T>(T toRecycle, Action<T> onRecycle) where T : IPoolable
    {
        onRecycle(toRecycle);
        toRecycle.Self.SetActive(false);
        pools[toRecycle.Prefab.Self].Recycle(toRecycle.Self);
    }
    /// <summary>
    /// Gets and activates an instance from the pool of the given ipoolable
    /// </summary>
    /// <typeparam name="T">ipoolable type</typeparam>
    /// <param name="original">instance</param>
    /// <returns>instance from the pool</returns>
    public static T Get<T>(T original) where T : IPoolable
    {
        T toGet = pools[original.Prefab.Self].Get(original.Prefab.Self).GetComponent<T>();
        toGet.Prefab = original.Prefab;
        toGet.Self.SetActive(true);
        return toGet;
    }
    /// <summary>
    /// Gets and activates an instance from the pool of the given ipoolable
    /// </summary>
    /// <typeparam name="T">ipoolable type</typeparam>
    /// <param name="original">instance</param>
    /// <param name="onGet">action called on getted instance before activation</param>
    /// <returns>instance from the pool</returns>
    public static T Get<T>(T original, Action<T> onGet) where T : IPoolable
    {
        T toGet = pools[original.Prefab.Self].Get(original.Prefab.Self).GetComponent<T>();
        toGet.Prefab = original.Prefab;
        onGet(toGet);
        toGet.Self.SetActive(true);
        return toGet;
    }
    /// <summary>
    /// Clears the pool of the given ipoolable, destroying the elements stored
    /// </summary>
    /// <typeparam name="T">ipoolable type</typeparam>
    /// <param name="original">instance</param>
    public static void Clear<T>(T original) where T : IPoolable
    {
        pools[original.Prefab.Self].Clear(onDestroy);
    }
    /// <summary>
    /// Destroys the pool of the given ipoolable, destroying the elements stored
    /// </summary>
    /// <typeparam name="T">ipoolable type</typeparam>
    /// <param name="original">instance</param>
    public static void Destroy<T>(T original) where T : IPoolable
    {
        pools[original.Prefab.Self].Clear(onDestroy);
        pools.Remove(original.Prefab.Self);
    }
    /// <summary>
    /// Clears all pools , destroying the elements stored
    /// </summary>
    public static void ClearAll()
    {
        foreach (Pool<GameObject> item in pools.Values)
            item.Clear(onDestroy);
    }
    /// <summary>
    /// Destroys all initializzated pools , destroying the elements stored
    /// </summary>
    public static void DestroyAll()
    {
        foreach (Pool<GameObject> item in pools.Values)
            item.Clear(onDestroy);
        pools.Clear();
    }
}