using UnityEngine;
using System;
using SOPRO;
using SOPRO.Variables;
using SOPRO.Events;
[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Coin : MonoBehaviour
{
    [NonSerialized]
    public SOPool Pool;

    public LayerHolder CoinLayer;
    public LayerHolder MonsterLayer;
    public LayerHolder ObjDestroyerLayer;

    public ReferenceInt CoinValue;
    public SOEvInt Event;

    public bool IsRecycled { get; private set; }

    private GameObject myGameObject;

    public void Collected()
    {
        this.Event.Raise(this.CoinValue.Value);
        this.Pool.Recycle(myGameObject);
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
            this.Pool.Recycle(myGameObject);
        }
    }
    void Start()
    {
        this.gameObject.layer = CoinLayer.LayerIndex;
        myGameObject = gameObject;
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