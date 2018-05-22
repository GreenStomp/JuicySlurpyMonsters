using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections.Generic;
[ExecuteInEditMode]
public class PlatformEditor : MonoBehaviour
{
    public Transform TerrainPrefab;
    public Transform SideTerrainPrefab;
    public Transform WaypointPrefab;
    public Text InfoShower;

    public string TerrainName = "Terrain";
    public string SideTerrainName = "SideTerrain";
    public string StartName = "Start";
    public string BezierP1Name = "BezierP1";
    public string BezierP2Name = "BezierP2";
    public string EndName = "End";
    public string GeneratedMeshPath = "Assets/GeneratedMeshes/";
    public string GeneratedTerrainMeshPath = "Assets/GeneratedMeshes/Terrains/";

    public Vector2 TerrainSize = new Vector2(20f, 5f);
    public Vector2 SideTerrainStepSize = new Vector2(1f, 0.2f);
    public float SideTerrainLength = 40f;
    [Range(0.01f, 50f)]
    public float DistanceForNewGeneration = 2f;
    public bool GenerateNewMesh = false;
    public bool SaveGeneratedMesh = false;
    [Range(BezierCurve.MinValidPoints, BezierCurve.MaxValidPoints)]
    public int NewMeshValidPoints = BezierCurve.MinValidPoints;
    public Transform ToEdit;
    private int prevToEditInstanceId;

    private BezierCurveShower shower;
    private BezierCurve curve;
    private Transform terrain;
    private Transform sideTerrain;
    private Transform start;
    private Transform p1;
    private Transform p2;
    private Transform end;

    private delegate void State();
    private State currentState;

    #region States
    void WaitNewEditObjState()
    {
        SaveGeneratedMesh = false;
        GenerateNewMesh = false;
        sideTerrain = null;
        end = null;
        start = null;
        terrain = null;
        p1 = null;
        p2 = null;
        curve = new BezierCurve();
        shower.enabled = false;

        if (ToEdit != null)
        {
            InitializeObj();
            currentState = IdleState;
        }
        else
            currentState = WaitNewEditObjState;
    }
    void IdleState()
    {
        if (ToEdit == null)
        {
            currentState = WaitNewEditObjState;
            return;
        }

        if (ToEdit.GetInstanceID() != prevToEditInstanceId)
            InitializeObj();

        curve.Set(start.position, p1.position, p2.position, end.position, NewMeshValidPoints);
        curve.ForceUpdateLenghts();

        if (GenerateNewMesh)
            GenerateMesh();

        if (SaveGeneratedMesh)
            SaveMesh();

        currentState = IdleState;
    }
    #endregion

    public void GenerateMesh()
    {
        GenerateNewMesh = false;

        Transform waypoint = GameObject.Instantiate<GameObject>(WaypointPrefab.gameObject).transform;

        MeshFilter filterTerrain = terrain.GetComponent<MeshFilter>();
        if (filterTerrain != null)
        {
            GameObject.DestroyImmediate(filterTerrain);
        }
        filterTerrain = terrain.gameObject.AddComponent<MeshFilter>();

        Mesh meshTerrain = filterTerrain.sharedMesh;
        if (meshTerrain == null)
        {
            filterTerrain.mesh = new Mesh();
            meshTerrain = filterTerrain.sharedMesh;
        }
        meshTerrain.Clear();

        MeshFilter filterSideTerrain = sideTerrain.GetComponent<MeshFilter>();
        if (filterSideTerrain != null)
        {
            GameObject.DestroyImmediate(filterSideTerrain);
        }
        filterSideTerrain = sideTerrain.gameObject.AddComponent<MeshFilter>();

        Mesh meshSideTerrain = filterSideTerrain.sharedMesh;
        if (meshSideTerrain == null)
        {
            filterSideTerrain.mesh = new Mesh();
            meshSideTerrain = filterSideTerrain.sharedMesh;
        }
        meshSideTerrain.Clear();


        List<Vector3> verticesTerrain = new List<Vector3>();
        List<Vector2> uvsTerrain = new List<Vector2>();
        List<int> trianglesTerrain = new List<int>();
        List<Vector3> verticesSideTerrain = new List<Vector3>();
        List<Vector2> uvsSideTerrain = new List<Vector2>();
        List<int> trianglesSideTerrain = new List<int>();

        float percentageIncrement = DistanceForNewGeneration * curve.InverseLength;
        float current = percentageIncrement;
        bool exit = false;

        #region TerrainGeneration
        Platform plat = ToEdit.GetComponent<Platform>();
        if (plat == null)
            plat = ToEdit.gameObject.AddComponent<Platform>();

        plat.SetPlatform(GetFirstAvailableId(), start, p1, p2, end, terrain, NewMeshValidPoints);

        RectCorners lastRect = new RectCorners();
        RectCorners nextRect = new RectCorners();
        LineCorners lastLine = new LineCorners();
        LineCorners nextLine = new LineCorners();
        Vector3 lastCenter = Vector3.zero;
        Vector3 lastUp = start.up;
        Vector3 a = plat.CalculateBezierCurve(0.001f);
        Vector3 b = plat.CalculateBezierCurve(-0.001f);
        Vector3 dir = (a - b).normalized;
        waypoint.position = lastCenter;
        waypoint.LookAt(dir + lastCenter, lastUp);
        Vector3 lastForward = waypoint.forward;
        Vector3 p1Pos = p1.position;
        Vector3 p2Pos = p2.position;
        Vector3 endPos = end.position;
        start.rotation = waypoint.rotation;
        p1.position = p1Pos;
        p2.position = p2Pos;
        end.position = endPos;
        terrain.localPosition = Vector3.zero;
        terrain.rotation = Quaternion.identity;

        Vector3 lastRight = waypoint.right;

        lastLine.GenerateNewLineCorners(lastCenter, TerrainSize.x, lastUp, lastRight);
        lastRect.GenerateNewRectCorners(lastCenter, TerrainSize, lastUp, lastRight);

        lastRect.GenerateMeshV(true, verticesTerrain);

        while (!exit)
        {
            Vector3 center;
            Vector3 right;
            Vector3 up;
            Vector3 forward;
            a = plat.CalculateBezierCurve(current + 0.001f);
            b = plat.CalculateBezierCurve(current - 0.001f);
            dir = (a - b).normalized;
            if (current >= 1f)
            {
                up = end.up;
                exit = true;
                center = end.position - start.position;
                waypoint.position = center;
                waypoint.LookAt(dir + center, up);
                forward = waypoint.forward;
                right = waypoint.right;
                end.rotation = waypoint.rotation;
            }
            else
            {
                up = Vector3.Lerp(start.up, end.up, current);
                center = plat.CalculateBezierCurve(current) - start.position;
                waypoint.position = center;
                waypoint.LookAt(dir + center, up);
                forward = waypoint.forward;
                right = waypoint.right;
            }

            nextLine.GenerateNewLineCorners(center, TerrainSize.x, up, right);
            nextRect.GenerateNewRectCorners(center, TerrainSize, up, right);

            if (Mathf.Approximately(1f, Vector3.Dot(right, lastRight)) && Mathf.Approximately(1f, Vector3.Dot(up, lastUp)) && Mathf.Approximately(1f, Vector3.Dot(forward, lastForward)) && !exit)
            {
                current += percentageIncrement;
                continue;
            }

            lastLine.GenerateConnectionMeshV(nextLine, SideTerrainStepSize, SideTerrainLength, verticesSideTerrain);
            lastRect.GenerateConnectionMeshV(nextRect, verticesTerrain);

            lastCenter = center;
            lastRight = right;
            lastUp = up;
            lastForward = forward;

            lastRect.Copy(nextRect);
            lastLine.Copy(nextLine);

            current += percentageIncrement;
        }

        lastRect.GenerateMeshV(false, verticesTerrain);

        for (int i = 0; i < verticesTerrain.Count; i += 6)
        {
            uvsTerrain.Add(new Vector2(0f, 0f));
            uvsTerrain.Add(new Vector2(1f, 0f));
            uvsTerrain.Add(new Vector2(0f, 1f));

            uvsTerrain.Add(new Vector2(0f, 1f));
            uvsTerrain.Add(new Vector2(1f, 0f));
            uvsTerrain.Add(new Vector2(1f, 1f));
        }

        for (int i = 0; i < verticesSideTerrain.Count; i += 6)
        {
            uvsSideTerrain.Add(new Vector2(0f, 0f));
            uvsSideTerrain.Add(new Vector2(1f, 0f));
            uvsSideTerrain.Add(new Vector2(0f, 1f));

            uvsSideTerrain.Add(new Vector2(0f, 1f));
            uvsSideTerrain.Add(new Vector2(1f, 0f));
            uvsSideTerrain.Add(new Vector2(1f, 1f));
        }

        for (int i = 0; i < verticesTerrain.Count; i++)
        {
            trianglesTerrain.Add(i);
        }

        for (int i = 0; i < verticesSideTerrain.Count; i++)
        {
            trianglesSideTerrain.Add(i);
        }

        GameObject.DestroyImmediate(waypoint.gameObject);
        #endregion

        meshTerrain.vertices = verticesTerrain.ToArray();
        meshTerrain.triangles = trianglesTerrain.ToArray();
        meshTerrain.uv = uvsTerrain.ToArray();
        meshTerrain.RecalculateNormals();
        meshTerrain.RecalculateBounds();
        meshTerrain.RecalculateTangents();

        meshSideTerrain.vertices = verticesSideTerrain.ToArray();
        meshSideTerrain.triangles = trianglesSideTerrain.ToArray();
        meshSideTerrain.uv = uvsSideTerrain.ToArray();
        meshSideTerrain.RecalculateNormals();
        meshSideTerrain.RecalculateBounds();
        meshSideTerrain.RecalculateTangents();
    }
    public void SaveMesh()
    {
        SaveGeneratedMesh = false;

        string pathTerrain = GeneratedMeshPath + ToEdit.gameObject.name + "_" + terrain.gameObject.name + ".asset";
        string pathSideTerrain = GeneratedTerrainMeshPath + ToEdit.gameObject.name + "_" + sideTerrain.gameObject.name + ".asset";
        Mesh meshTerrain = terrain.GetComponent<MeshFilter>().sharedMesh;
        Mesh meshSideTerrain = sideTerrain.GetComponent<MeshFilter>().sharedMesh;
        if (meshTerrain == null)
        {
            throw new UnityException("Failed to save generate terrain mesh. No mesh found");
        }
        if (meshSideTerrain == null)
        {
            throw new UnityException("Failed to save generate side terrain mesh. No mesh found");
        }
        if (AssetDatabase.Contains(meshTerrain))
        {
            throw new UnityException("Failed to save generate terrain mesh. Mesh has already been saved");
        }
        if (AssetDatabase.Contains(meshSideTerrain))
        {
            throw new UnityException("Failed to save generate side terrain mesh. Mesh has already been saved");
        }
        AssetDatabase.CreateAsset(meshTerrain, pathTerrain);
        AssetDatabase.CreateAsset(meshSideTerrain, pathSideTerrain);
    }
    public void UpdateAllPlatformId(ref List<Platform> platforms)
    {
        bool finished = platforms.Count == 0;
        while (!finished)
        {
            finished = true;

            platforms = platforms.OrderBy(x => x.ID).ToList();

            uint prevId = platforms[0].ID;

            for (int i = 1; i < platforms.Count; i++)
            {
                Platform current = platforms[i];

                if (current.ID == prevId)
                {
                    uint firstId = 0;

                    for (int j = 0; j < platforms.Count; j++)
                    {
                        Platform temp = platforms[j];
                        if (firstId == temp.ID)
                        {
                            firstId++;
                            j = -1;
                        }
                    }

                    current.SetPlatform(firstId);

                    finished = false;
                }
                else
                {
                    prevId = current.ID;
                }
            }
        }
    }
    public uint GetFirstAvailableId()
    {
        List<Platform> allPlatformsPrefabs = Resources.LoadAll<Platform>(string.Empty).ToList();

        UpdateAllPlatformId(ref allPlatformsPrefabs);

        allPlatformsPrefabs = allPlatformsPrefabs.OrderBy(x => x.ID).ToList();

        uint first = 0;

        for (int i = 0; i < allPlatformsPrefabs.Count; i++)
        {
            Platform current = allPlatformsPrefabs[i];
            if (first == current.ID)
                first++;
        }

        allPlatformsPrefabs = null;

        Resources.UnloadUnusedAssets();

        return first;
    }
    public void InitializeObj()
    {
        prevToEditInstanceId = ToEdit.GetInstanceID();
        shower.enabled = true;
        Transform[] childs = ToEdit.GetComponentsInChildren<Transform>();

        terrain = childs.FirstOrDefault(x => x.gameObject.name == TerrainName);
        if (terrain == null)
        {
            terrain = GameObject.Instantiate<GameObject>(TerrainPrefab.gameObject).transform;
            terrain.gameObject.name = TerrainName;
            terrain.parent = ToEdit;
            terrain.localPosition = Vector3.zero;
        }

        sideTerrain = childs.FirstOrDefault(x => x.gameObject.name == SideTerrainName);
        if (sideTerrain == null)
        {
            sideTerrain = GameObject.Instantiate<GameObject>(SideTerrainPrefab.gameObject).transform;
            sideTerrain.gameObject.name = SideTerrainName;
            sideTerrain.parent = ToEdit;
            sideTerrain.localPosition = Vector3.zero;
        }

        start = childs.FirstOrDefault(x => x.gameObject.name == StartName);
        if (start == null)
        {
            start = GameObject.Instantiate<GameObject>(WaypointPrefab.gameObject).transform;
            start.gameObject.name = StartName;
            start.parent = ToEdit;
            start.localPosition = Vector3.zero;
        }

        p1 = childs.FirstOrDefault(x => x.gameObject.name == BezierP1Name);
        if (p1 == null)
        {
            p1 = GameObject.Instantiate<GameObject>(WaypointPrefab.gameObject).transform;
            p1.gameObject.name = BezierP1Name;
            p1.parent = ToEdit;
            p1.localPosition = Vector3.zero;
        }

        p2 = childs.FirstOrDefault(x => x.gameObject.name == BezierP2Name);
        if (p2 == null)
        {
            p2 = GameObject.Instantiate<GameObject>(WaypointPrefab.gameObject).transform;
            p2.gameObject.name = BezierP2Name;
            p2.parent = ToEdit;
            p2.localPosition = Vector3.zero;
        }

        end = childs.FirstOrDefault(x => x.gameObject.name == EndName);
        if (end == null)
        {
            end = GameObject.Instantiate<GameObject>(WaypointPrefab.gameObject).transform;
            end.gameObject.name = EndName;
            end.parent = ToEdit;
            end.localPosition = Vector3.zero;
        }

        Platform p = ToEdit.GetComponent<Platform>();
        if (p != null)
            NewMeshValidPoints = p.CurveValidPoints;
    }

    void Update()
    {
        if (shower == null)
            shower = GetComponent<BezierCurveShower>();
        if (curve == null)
            curve = new BezierCurve();
        if (currentState == null)
            currentState = IdleState;

        currentState.Invoke();

        if (shower != null)
        {
            if (shower.CurveToDraw == null)
                shower.CurveToDraw = new BezierCurve();
            shower.CurveToDraw.Copy(curve);
            shower.CurveToDraw.ForceUpdateLenghts();
        }
    }
    private class LineCorners
    {
        public Vector3 Left { get; private set; }
        public Vector3 Right { get; private set; }
        public Vector3 UpDir { get; private set; }
        public void GenerateNewLineCorners(Vector3 upCenterPos, float distance, Vector3 upDir, Vector3 rightDir)
        {
            Right = upCenterPos + rightDir * distance * 0.5f;
            Left = upCenterPos - rightDir * distance * 0.5f;
            UpDir = upDir;
        }
        public void GenerateConnectionMeshV(LineCorners next, Vector2 stepSize, float length, List<Vector3> vertices)
        {
            //Right side
            Vector3 nextUpPoint = next.Right + stepSize.y * next.UpDir + stepSize.x * (next.Right - next.Left).normalized;
            Vector3 nextEndPoint = nextUpPoint + length * (next.Right - next.Left).normalized;
            Vector3 upPoint = Right + stepSize.y * UpDir + stepSize.x * (Right - Left).normalized;
            Vector3 endPoint = upPoint + length * (Right - Left).normalized;

            //step
            vertices.Add(next.Right);
            vertices.Add(nextUpPoint);
            vertices.Add(Right);

            vertices.Add(Right);
            vertices.Add(nextUpPoint);
            vertices.Add(upPoint);
            //plane
            vertices.Add(nextUpPoint);
            vertices.Add(nextEndPoint);
            vertices.Add(upPoint);

            vertices.Add(upPoint);
            vertices.Add(nextEndPoint);
            vertices.Add(endPoint);

            //Left Side
            nextUpPoint = next.Left + stepSize.y * next.UpDir + stepSize.x * (next.Left - next.Right).normalized;
            nextEndPoint = nextUpPoint + length * (next.Left - next.Right).normalized;
            upPoint = Left + stepSize.y * UpDir + stepSize.x * (Left - Right).normalized;
            endPoint = upPoint + length * (Left - Right).normalized;

            //step
            vertices.Add(Left);
            vertices.Add(upPoint);
            vertices.Add(next.Left);

            vertices.Add(next.Left);
            vertices.Add(upPoint);
            vertices.Add(nextUpPoint);
            //plane
            vertices.Add(upPoint);
            vertices.Add(endPoint);
            vertices.Add(nextUpPoint);

            vertices.Add(nextUpPoint);
            vertices.Add(endPoint);
            vertices.Add(nextEndPoint);
        }
        public void Copy(LineCorners other)
        {
            Left = other.Left;
            Right = other.Right;
            UpDir = other.UpDir;
        }
    }
    private class RectCorners
    {
        public Vector3 UpLeft { get; private set; }
        public Vector3 UpRight { get; private set; }
        public Vector3 DownLeft { get; private set; }
        public Vector3 DownRight { get; private set; }
        public void GenerateNewRectCorners(Vector3 upCenterPos, Vector2 rectSize, Vector3 upDir, Vector3 rightDir)
        {
            Vector2 correctSize = new Vector2(rectSize.x * 0.5f, rectSize.y);
            UpLeft = upCenterPos - rightDir * correctSize.x;
            UpRight = upCenterPos + rightDir * correctSize.x;
            DownLeft = UpLeft - upDir * correctSize.y;
            DownRight = UpRight - upDir * correctSize.y;
        }
        public void GenerateMeshV(bool isRectStart, List<Vector3> vertices)
        {
            if (!isRectStart)
            {
                vertices.Add(DownRight);
                vertices.Add(UpRight);
                vertices.Add(DownLeft);

                vertices.Add(DownLeft);
                vertices.Add(UpRight);
                vertices.Add(UpLeft);
            }
            else
            {
                vertices.Add(DownLeft);
                vertices.Add(UpLeft);
                vertices.Add(DownRight);

                vertices.Add(DownRight);
                vertices.Add(UpLeft);
                vertices.Add(UpRight);
            }
        }
        public void GenerateConnectionMeshV(RectCorners next, List<Vector3> vertices)
        {
            //left face
            vertices.Add(next.DownLeft);
            vertices.Add(next.UpLeft);
            vertices.Add(DownLeft);

            vertices.Add(DownLeft);
            vertices.Add(next.UpLeft);
            vertices.Add(UpLeft);
            //up face
            vertices.Add(next.UpLeft);
            vertices.Add(next.UpRight);
            vertices.Add(UpLeft);

            vertices.Add(UpLeft);
            vertices.Add(next.UpRight);
            vertices.Add(UpRight);
            //right face
            vertices.Add(DownRight);
            vertices.Add(UpRight);
            vertices.Add(next.DownRight);

            vertices.Add(next.DownRight);
            vertices.Add(UpRight);
            vertices.Add(next.UpRight);
            //down face
            vertices.Add(DownLeft);
            vertices.Add(DownRight);
            vertices.Add(next.DownLeft);

            vertices.Add(next.DownLeft);
            vertices.Add(DownRight);
            vertices.Add(next.DownRight);
        }
        public void Copy(RectCorners toCopy)
        {
            UpLeft = toCopy.UpLeft;
            UpRight = toCopy.UpRight;
            DownLeft = toCopy.DownLeft;
            DownRight = toCopy.DownRight;
        }
    }
}