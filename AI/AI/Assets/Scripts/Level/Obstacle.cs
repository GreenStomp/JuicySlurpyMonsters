using UnityEngine;
[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public abstract class Obstacle : MonoBehaviour
{
    void Start()
    {
        this.gameObject.layer = GameManager.ObstacleLayer;
    }
    void Reset()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
        GetComponent<Collider>().isTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        int otherLayer = other.gameObject.layer;
        //Monster layer
        if ((otherLayer & GameManager.MonsterLayer) == GameManager.MonsterLayer)
            OnMonsterTriggerEnter(other);
        //human layer
        if ((otherLayer & GameManager.HumanLayer) == GameManager.HumanLayer)
            OnHumanTriggerEnter(other);
        //obstacle destroyer layer
        if ((otherLayer & GameManager.ObstacleDestroyerLayer) == GameManager.ObstacleDestroyerLayer)
            OnObstacleDestroyerTriggerEnter(other);
    }
    private void OnTriggerExit(Collider other)
    {
        int otherLayer = other.gameObject.layer;
        //Monster layer
        if ((otherLayer & GameManager.MonsterLayer) == GameManager.MonsterLayer)
            OnMonsterTriggerExit(other);
        //human layer
        if ((otherLayer & GameManager.HumanLayer) == GameManager.HumanLayer)
            OnHumanTriggerExit(other);
        //obstacle destroyer layer
        if ((otherLayer & GameManager.ObstacleDestroyerLayer) == GameManager.ObstacleDestroyerLayer)
            OnObstacleDestroyerTriggerExit(other);
    }
    protected abstract void OnMonsterTriggerEnter(Collider monster);
    protected abstract void OnHumanTriggerEnter(Collider human);
    protected abstract void OnObstacleDestroyerTriggerEnter(Collider human);
    protected abstract void OnMonsterTriggerExit(Collider monster);
    protected abstract void OnHumanTriggerExit(Collider human);
    protected abstract void OnObstacleDestroyerTriggerExit(Collider human);
}