using UnityEngine;
using System;
public class Coin : MonoBehaviour
{
    [NonSerialized]
    public PoolCoin Pool;

    public ReferenceInt Value;
    public SOEvInt Event;

    public LayerHolder MonsterLayer;
    public LayerHolder ObjDestroyerLayer;

    public bool IsRecycled { get; private set; }

    public void Collected()
    {
        this.Event.Raise(this.Value.Value);
        this.Pool.Recycle(this);
    }
    void OnEnable()
    {
        this.IsRecycled = false;
    }
    void OnDisable()
    {
        this.IsRecycled = true;
    }
    void OnTriggerEnter(Collider collider)
    {
        int collidedLayer = collider.gameObject.layer;
        
        if (collidedLayer == this.MonsterLayer.LayerIndex)
        {
            this.Collected();
        }
        else if (collidedLayer == this.ObjDestroyerLayer.LayerIndex)
        {
            this.Pool.Recycle(this);
        }
    }
}