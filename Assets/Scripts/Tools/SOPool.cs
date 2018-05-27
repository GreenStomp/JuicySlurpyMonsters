using System;
using System.Collections.Generic;
using UnityEngine;
public class SOPool<T> : ScriptableObject where T : Component
{
    /// <summary>
    /// Prefab instance used by the pool
    /// </summary>
    public T Prefab { get { return prefab; } set { prefab = value; } }
    /// <summary>
    /// Number of elements stored in the pool
    /// </summary>
    public int ElementsStored { get { return elements.Count; } }

    private Queue<T> elements = new Queue<T>();

    [SerializeField]
    private T prefab;

    /// <summary>
    /// Recycles the given instance
    /// </summary>
    /// <param name="toRecycle">object to recycle</param>
    public void Recycle(T toRecycle)
    {
        elements.Enqueue(toRecycle);
        toRecycle.gameObject.SetActive(false);
    }
    /// <summary>
    /// Recycles the given instance. No further action will be performed on the object
    /// </summary>
    /// <param name="toRecycle">object to recycle</param>
    public void DirectRecycle(T toRecycle)
    {
        elements.Enqueue(toRecycle);
    }
    /// <summary>
    /// Recycles the given instance
    /// </summary>
    /// <param name="toRecycle">object to recycle</param>
    /// <param name="onRecycle">action called on element after deactivation</param>
    public void Recycle(T toRecycle, Action<T> onRecycle)
    {
        elements.Enqueue(toRecycle);
        toRecycle.gameObject.SetActive(false);

        onRecycle(toRecycle);
    }
    /// <summary>
    /// Requests an element from the pool.
    /// </summary>
    /// <param name="onGet">action called on element before activation</param>
    /// <returns>the requested element instance</returns>
    public T Get(Action<T> onGet)
    {
        T res = elements.Count == 0 ? GameObject.Instantiate(prefab) : elements.Dequeue();
        onGet(res);
        res.gameObject.SetActive(true);
        return res;
    }
    /// <summary>
    /// Requests an element from the pool.
    /// </summary>
    /// <returns>the requested element instance</returns>
    public T Get()
    {
        T res = elements.Count == 0 ? GameObject.Instantiate(prefab) : elements.Dequeue();
        res.gameObject.SetActive(true);
        return res;
    }
    /// <summary>
    /// Requests an element from the pool. No further action will be performed on the object
    /// </summary>
    /// <returns>the requested element instance</returns>
    public T DirectGet()
    {
        return elements.Count == 0 ? GameObject.Instantiate(prefab) : elements.Dequeue();
    }
    /// <summary>
    /// Requests an element from the pool.
    /// </summary>
    /// <param name="parent">transform to use as the requested element parent. Used only on instantiated elements</param>
    /// <param name="onGet">action called on element before activation</param>
    /// <returns>the requested element instance</returns>
    public T Get(Transform parent, Action<T> onGet)
    {
        T res = elements.Count == 0 ? GameObject.Instantiate(prefab, parent) : elements.Dequeue();
        onGet(res);
        res.gameObject.SetActive(true);
        return res;
    }
    /// <summary>
    /// Requests an element from the pool.
    /// </summary>
    /// <param name="parent">transform to use as the requested element parent. Used only on instantiated elements</param>
    /// <returns>the requested element instance</returns>
    public T Get(Transform parent)
    {
        T res = elements.Count == 0 ? GameObject.Instantiate(prefab, parent) : elements.Dequeue();
        res.gameObject.SetActive(true);
        return res;
    }
    /// <summary>
    /// Requests an element from the pool.
    /// </summary>
    /// <param name="parent">transform to use as the requested element parent. Used only on instantiated elements</param>
    /// <param name="instantiateInWorldSpace">determines whenever the requested elements should be spaned in wold space coordinates. Used only on instantiated elements</param>
    /// <param name="onGet">action called on element before activation</param>
    /// <returns>the requested element instance</returns>
    public T Get(Transform parent, bool instantiateInWorldSpace, Action<T> onGet)
    {
        T res = elements.Count == 0 ? GameObject.Instantiate(prefab, parent, instantiateInWorldSpace) : elements.Dequeue();
        onGet(res);
        res.gameObject.SetActive(true);
        return res;
    }
    /// <summary>
    /// Requests an element from the pool.
    /// </summary>
    /// <param name="parent">transform to use as the requested element parent. Used only on instantiated elements</param>
    /// <param name="instantiateInWorldSpace">determines whenever the requested elements should be spaned in wold space coordinates. Used only on instantiated elements</param>
    /// <returns>the requested element instance</returns>
    public T Get(Transform parent, bool instantiateInWorldSpace)
    {
        T res = elements.Count == 0 ? GameObject.Instantiate(prefab, parent, instantiateInWorldSpace) : elements.Dequeue();
        res.gameObject.SetActive(true);
        return res;
    }
    /// <summary>
    /// Clears the pool invoking an action on each element
    /// </summary>
    /// <param name="onDestroy">action invoked on each element in the pool</param>
    public void Clear(Action<T> onDestroy)
    {
        while (elements.Count != 0)
        {
            T obj = elements.Dequeue();
            onDestroy(obj);
            GameObject.Destroy(obj.gameObject);
        }
    }
    /// <summary>
    /// Clears the pool invoking an action on each element
    /// </summary>
    public void Clear()
    {
        while (elements.Count != 0)
        {
            T obj = elements.Dequeue();
            GameObject.Destroy(obj.gameObject);
        }
    }
    /// <summary>
    /// Resizes the pool to the given length, invoking an action on each destroyed element (if there are any) and each created element (if there are any)
    /// </summary>
    /// <param name="onDestroy">action invoked on each destroyed element in the pool</param>
    /// <param name="parent">transform to use as the requested element parent. Used only on instantiated elements</param>
    /// <param name="instantiateInWorldSpace">determines whenever the requested elements should be spaned in wold space coordinates. Used only on instantiated elements</param>
    /// <param name="onRecycle">action called on element after deactivation</param>
    /// <param name="value">target length</param>
    public void ReSize(uint value, Action<T> onDestroy = null, Transform parent = null, bool instantiateInWorldSpace = true, Action<T> onRecycle = null)
    {
        while (elements.Count > value)
        {
            T obj = elements.Dequeue();
            if (onDestroy != null)
                onDestroy(obj);
            GameObject.Destroy(obj.gameObject);
        }
        while (elements.Count < value)
        {
            T obj = GameObject.Instantiate(prefab, parent, instantiateInWorldSpace);
            obj.gameObject.SetActive(false);
            if (onRecycle != null)
                onRecycle(obj);
            elements.Enqueue(obj);
        }
    }
}