using UnityEngine;
using System;
[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Coin : MonoBehaviour
{
    [NonSerialized]
    public PoolCoin Pool;

    public LayerHolder CoinLayer;
    public LayerHolder MonsterLayer;
    public LayerHolder ObjDestroyerLayer;

    public ReferenceInt Value;
    public SOEvInt Event;

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
    void Start()
    {
        this.gameObject.layer = CoinLayer.LayerIndex;
    }
    void Reset()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
        rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
        GetComponent<Collider>().isTrigger = true;
    }
}