using UnityEngine;
using System;
using SOPRO;
public class Coin : MonoBehaviour
{
    [NonSerialized]
    public SOPool Pool;

    public SOVariableUint CoinsPickedCounter;
    public SOVariableUint CoinsMissedCounter;
    public SOVariableUint CoinsPickedValueCounter;
    public SOVariableUint CoinsMissedValueCounter;

    public LayerHolder CoinLayer;
    public LayerHolder MonsterLayer;
    public LayerHolder ObjDestroyerLayer;

    public ReferenceUint CoinValue;

    public bool IsRecycled { get; private set; }

    [SerializeField]
    private Rigidbody bd;
    [SerializeField]
    private Collider coll;

    private GameObject myGameObject;

    public void Collected()
    {
        CoinsPickedCounter.Value++;
        CoinsPickedValueCounter.Value += CoinValue.Value;
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
            CoinsMissedCounter.Value++;
            CoinsMissedValueCounter.Value += CoinValue.Value;
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
        bd.useGravity = false;
        bd.isKinematic = true;
        bd.collisionDetectionMode = CollisionDetectionMode.Discrete;
        coll.isTrigger = true;
    }
}