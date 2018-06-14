using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using SOPRO;
// Tagging component for use with the LocalNavMeshBuilder
// Supports mesh-filter and terrain - can be extended to physics and/or primitives
[DefaultExecutionOrder(-200)]
public class NavMeshSourceTag : MonoBehaviour
{
    // Global containers for all active mesh/terrain tags
    public SOLinkedListMeshFilterContainer M_Meshes;
    public SOLinkedListTerrainContainer M_Terrains;

    public MeshFilter Filter;
    private LinkedListNode<MeshFilter> meshNode;

    public Terrain Terrain;
    private LinkedListNode<Terrain> terrainNode;

    private void Awake()
    {
        if (!Filter)
            Filter = GetComponent<MeshFilter>();

        if (!Terrain)
            Terrain = GetComponent<Terrain>();
    }

    void OnEnable()
    {
        if (Filter)
            meshNode = M_Meshes.Elements.AddLast(Filter);

        if (Terrain)
            terrainNode = M_Terrains.Elements.AddLast(Terrain);
    }

    void OnDisable()
    {
        if (meshNode != null)
            M_Meshes.Elements.Remove(meshNode);

        if (terrainNode != null)
            M_Terrains.Elements.Remove(terrainNode);
    }

    // Collect all the navmesh build sources for enabled objects tagged by this component
    public static void Collect(ref List<NavMeshBuildSource> sources, SOLinkedListMeshFilterContainer m_Meshes, SOLinkedListTerrainContainer m_Terrains)
    {
        sources.Clear();

        LinkedListNode<MeshFilter> currentMF = m_Meshes.Elements.First;
        while (currentMF != null)
        {
            MeshFilter filter = currentMF.Value;
            LinkedListNode<MeshFilter> next = currentMF.Next;

            if (!currentMF.Value)
            {
                m_Meshes.Elements.Remove(currentMF);
                currentMF = next;
                continue;
            }

            Mesh m = filter.sharedMesh;
            if (!m) continue;

            NavMeshBuildSource s = new NavMeshBuildSource();
            s.shape = NavMeshBuildSourceShape.Mesh;
            s.sourceObject = m;
            s.transform = filter.transform.localToWorldMatrix;
            s.area = 0;
            sources.Add(s);

            currentMF = next;
        }

        LinkedListNode<Terrain> currentT = m_Terrains.Elements.First;
        while (currentT != null)
        {
            Terrain t = currentT.Value;
            LinkedListNode<Terrain> next = currentT.Next;

            if (!t)
            {
                m_Terrains.Elements.Remove(currentT);
                currentT = next;
                continue;
            }

            var s = new NavMeshBuildSource();
            s.shape = NavMeshBuildSourceShape.Terrain;
            s.sourceObject = t.terrainData;
            // Terrain system only supports translation - so we pass translation only to back-end
            s.transform = Matrix4x4.TRS(t.transform.position, Quaternion.identity, Vector3.one);
            s.area = 0;
            sources.Add(s);

            currentT = next;
        }
    }
}
