using UnityEngine;
[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Coin : MonoBehaviour, IPoolable
{
    public IPoolable Prefab { get; set; }
    /// <summary>
    /// Returns coin gameobject. Faster than accessing coin.gameObject
    /// </summary>
    public GameObject Self { get { if (self == null) self = this.gameObject; return this.self; } }
    /// <summary>
    /// Returns coin position. Faster than accessing transform.position
    /// </summary>
    public Vector3 Position { get { return transf.position; } }

    private GameObject self;
    private Transform transf;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == GameManager.MonsterLayer)
            this.self.SetActive(false);
    }
    void Awake()
    {
        self = this.gameObject;
        transf = GetComponent<Transform>();
    }
    //void Reset()
    //{
    //    Rigidbody rb = GetComponent<Rigidbody>();
    //    rb.useGravity = false;
    //    rb.isKinematic = true;
    //    rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    //    GetComponent<Collider>().isTrigger = true;
    //}
}