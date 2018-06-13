using UnityEngine;
using UnityEngine.AI;
using SOPRO;
[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshMovement : MonoBehaviour
{
    [SerializeField]
    private bool autoRepath = true;
    [SerializeField]
    private ReferenceFloat maxSpeed;
    [SerializeField]
    private ReferenceFloat recalculateDistance;
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private LayerMaskHolder layerMask;
    [SerializeField]
    private NavMeshAreaMaskHolder areaLayer;

    private Transform toMove;
    void OnEnable()
    {
        if (!toMove)
            toMove = agent.transform;


        NavMeshHit hit;
        if (NavMesh.SamplePosition(toMove.position, out hit, 1f, areaLayer))
        {
            Debug.Log("warped");
            agent.Warp(hit.position);
        }
        agent.speed = maxSpeed;
        agent.areaMask = areaLayer;
        agent.autoRepath = autoRepath;
    }
    void Start()
    {
        agent.updateRotation = true;
    }
    void Update()
    {
        if (agent.isOnNavMesh)
        {
            if (!agent.hasPath || agent.remainingDistance < recalculateDistance)
                CheckGroundStatus();
        }
        else
        {
            OnEnable();
        }
    }
    private void CheckGroundStatus()
    {
        Vector3 up = toMove.up;

        RaycastHit hit;
        Ray ray = new Ray(toMove.position + up, Vector3.Lerp(toMove.forward, -up, 0.5f).normalized);

        if (Physics.Raycast(ray, out hit, 10f, layerMask))
            agent.SetDestination(hit.collider.transform.root.GetComponentInChildren<Platform>().MiddleLaneEndPos);
    }
}