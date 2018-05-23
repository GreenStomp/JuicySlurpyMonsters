using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using NavMeshBuilder = UnityEngine.AI.NavMeshBuilder;

// Build and update a localized navmesh from the sources marked by NavMeshSourceTag
[DefaultExecutionOrder(-500)]
public class LocalNavMeshBuilder : MonoBehaviour
{
    // The center of the build
    public Transform m_Tracked;

    // The size of the build bounds
    public Vector3 m_Size = new Vector3(80.0f, 20.0f, 80.0f);

    //the rotation of the build bounds
    public Transform Player;
    public Quaternion Rot;
    public Vector3 Rot1;
    public float Radius;
    public MeshRenderer Mesh;
    public FollowPlayer followPlayer;

    NavMeshData m_NavMesh;
    AsyncOperation m_Operation;
    NavMeshDataInstance m_Instance;
    List<NavMeshBuildSource> m_Sources = new List<NavMeshBuildSource>();

    IEnumerator Start()
    {
        while (true)
        {
            UpdateNavMesh(true);
            yield return m_Operation;
        }
    }

    void OnEnable()
    {
        // Construct and add navmesh
        m_NavMesh = new NavMeshData();
        m_Instance = NavMesh.AddNavMeshData(m_NavMesh);
        if (m_Tracked == null)
            m_Tracked = transform;

        UpdateNavMesh(false);
    }

    void OnDisable()
    {
        // Unload navmesh and clear handle
        m_Instance.Remove();
    }

    private void LateUpdate()
    {
        //Mesh.transform.LookAt(followPlayer.WayPoints[followPlayer.Indexer].transform.localPosition);
        Mesh.transform.LookAt(followPlayer.EndPointsToAdd[1].transform.localPosition);
    }

    void UpdateNavMesh(bool asyncUpdate = false)
    {
        NavMeshSourceTag.Collect(ref m_Sources);
        var defaultBuildSettings = NavMesh.GetSettingsByID(0);
        var bounds = QuantizedBounds();

        if (asyncUpdate)
            m_Operation = NavMeshBuilder.UpdateNavMeshDataAsync(m_NavMesh, defaultBuildSettings, m_Sources, Mesh.bounds);
        else
            NavMeshBuilder.UpdateNavMeshData(m_NavMesh, defaultBuildSettings, m_Sources, Mesh.bounds);

        //NavMeshBuilder.UpdateNavMeshData()
    }

    static Vector3 Quantize(Vector3 v, Vector3 quant)
    {
        float x = quant.x * Mathf.Floor(v.x / quant.x);
        float y = quant.y * Mathf.Floor(v.y / quant.y);
        float z = quant.z * Mathf.Floor(v.z / quant.z);
        return new Vector3(x, y, z);
    }

    static Quaternion RotSize(Quaternion v,Quaternion quant)
    {
        float x = quant.x * Mathf.Floor(v.x / quant.x);
        float y = quant.y * Mathf.Floor(v.y / quant.y);
        float z = quant.z * Mathf.Floor(v.z / quant.z);
        float w = quant.w * Mathf.Floor(v.w / quant.w);
        return new Quaternion(x, y, z,w);
    }

    Bounds QuantizedBounds()
    {
        // Quantize the bounds to update only when theres a 10% change in size
        var center = m_Tracked ? m_Tracked.position : transform.position;
        return new Bounds(Quantize(center, 0.1f * m_Size), m_Size);
    }

    BoundingSphere RotSize()
    {
        Vector3 center = m_Tracked ? m_Tracked.localEulerAngles : transform.localEulerAngles;
        return new BoundingSphere(center*0.2f, 10f);
    }

    void OnDrawGizmosSelected()
    {
        //if (m_NavMesh)
        //{
        //    Gizmos.color = Color.green;
        //    Gizmos.DrawWireCube(m_NavMesh.sourceBounds.center, m_NavMesh.sourceBounds.size);
        //    //Gizmos.DrawWireSphere(m_NavMesh.sourceBounds.center, 100f);
        //}

        //var bounds = QuantizedBounds();
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireCube(bounds.center, bounds.size);


        //var spherBounds = RotSize();
        //Gizmos.color = Color.magenta;
        //Gizmos.DrawWireSphere(spherBounds.position, spherBounds.radius);

        //Gizmos.color = Color.green;
        //var center = m_Tracked ? m_Tracked.position : transform.position;
        //Gizmos.DrawWireCube(center, m_Size);
    }
}
