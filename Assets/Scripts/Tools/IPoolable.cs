using UnityEngine;
public interface IPoolable
{
    IPoolable Prefab { get; set; }
    GameObject Self { get; }
}