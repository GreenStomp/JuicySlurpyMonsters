using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.IO;
using System.Linq;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
public class FollowPlayer : MonoBehaviour
{
    private Transform navMeshBuilder;
    private Vector3 CurrentTarget;
    private NavMeshAgent navMesh;

    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        navMesh.updateRotation = true;
        navMeshBuilder = this.transform.GetChild(0);
    }
    void Update()
    {
        CheckGroundStatus();
        MoveState();

    }
    private void MoveState()
    {
        if (navMesh.isOnNavMesh && CurrentTarget != null)
        {
            navMesh.destination = CurrentTarget;
        }
    }
    private void CheckGroundStatus()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position + new Vector3(0f, 0f, 0.5f), new Vector3(0, -0.5f, 1));

        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit, 10f))
        {
            CurrentTarget = hit.collider.transform.root.GetComponentInChildren<Platform>().MiddleLaneEndPos;
        }
    }
}
