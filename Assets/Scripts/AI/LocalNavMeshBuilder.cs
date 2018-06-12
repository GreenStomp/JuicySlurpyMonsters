using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using NavMeshBuilder = UnityEngine.AI.NavMeshBuilder;
using SOPRO;
// Build and update a localized navmesh from the sources marked by NavMeshSourceTag
[DefaultExecutionOrder(-500)]
public class LocalNavMeshBuilder : MonoBehaviour
{
    // The center of the build
    public ReferenceVector3 Center;
    //public Transform m_Tracked;

    // The size of the build bounds
    public ReferenceVector3 m_Size;

    [SerializeField]
    private ReferenceBounds Bounds;
    [SerializeField]
    private SOLinkedListMeshFilterContainer meshes;
    [SerializeField]
    private SOLinkedListTerrainContainer terrains;

    NavMeshData m_NavMesh;
    AsyncOperation m_Operation;
    NavMeshDataInstance m_Instance;
    List<NavMeshBuildSource> m_Sources = new List<NavMeshBuildSource>();

    void Update()
    {
        UpdateNavMesh(false);
    }

    void OnEnable()
    {
        // Construct and add navmesh
        m_NavMesh = new NavMeshData();
        m_Instance = NavMesh.AddNavMeshData(m_NavMesh);
        //if (m_Tracked == null)
        //    m_Tracked = transform;

        UpdateNavMesh(false);
    }

    void OnDisable()
    {
        // Unload navmesh and clear handle
        m_Instance.Remove();
    }

    void UpdateNavMesh(bool asyncUpdate = false)
    {
        NavMeshSourceTag.Collect(ref m_Sources, meshes, terrains);
        NavMeshBuildSettings defaultBuildSettings = NavMesh.GetSettingsByID(0);
        Bounds bounds = QuantizedBounds();

        if (asyncUpdate)
            m_Operation = NavMeshBuilder.UpdateNavMeshDataAsync(m_NavMesh, defaultBuildSettings, m_Sources, Bounds);
        else
            NavMeshBuilder.UpdateNavMeshData(m_NavMesh, defaultBuildSettings, m_Sources, Bounds);

        //NavMeshBuilder.UpdateNavMeshData()
    }

    static Vector3 Quantize(Vector3 v, Vector3 quant)
    {
        float x = quant.x * Mathf.Floor(v.x / quant.x);
        float y = quant.y * Mathf.Floor(v.y / quant.y);
        float z = quant.z * Mathf.Floor(v.z / quant.z);
        return new Vector3(x, y, z);
    }

    //static Quaternion RotSize(Quaternion v, Quaternion quant)
    //{
    //    float x = quant.x * Mathf.Floor(v.x / quant.x);
    //    float y = quant.y * Mathf.Floor(v.y / quant.y);
    //    float z = quant.z * Mathf.Floor(v.z / quant.z);
    //    float w = quant.w * Mathf.Floor(v.w / quant.w);
    //    return new Quaternion(x, y, z, w);
    //}

    Bounds QuantizedBounds()
    {
        // Quantize the bounds to update only when theres a 10% change in size
        //Vector3 center = m_Tracked ? m_Tracked.position : transform.position;
        return new Bounds(Quantize(Center, 0.1f * m_Size.Value), m_Size);
    }

    //BoundingSphere RotSize()
    //{
    //    Vector3 center = m_Tracked ? m_Tracked.localEulerAngles : transform.localEulerAngles;
    //    return new BoundingSphere(center * 0.2f, 10f);
    //}
}
