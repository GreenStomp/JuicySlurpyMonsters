using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SOPRO;
using System.IO;
public class PlatformToolEditorWindow : EditorWindow
{
    public SOLinkedListMeshFilterContainer MeshFilters;
    public SOLinkedListTerrainContainer Terrains;
    public LayerHolder PlatformLayer;
    public LayerHolder SidePlatformLayer;
    public string FolderTarget = "Demo";
    public int ValidPoints = V3BezierCurve.MinValidPoints;
    public int LaneCount = 3;
    public float MeshPrecision = 1f;
    public float LaneDistance = 5f;
    public Vector2 PlatformSize = new Vector2(20, 5), StepSize = new Vector2(5, 0.5f);
    public float SidePlatformSize = 40f;
    public bool ShowLines = false;
    public Material DefaultMaterial;

    private Transform startPoint;
    private Transform p1Point;
    private Transform p2Point;
    private Transform endPoint;
    private LineRenderer[] lines;
    private Lane[] lanes;
    private GameObject platformRoot;
    private readonly Vector3[] points = new Vector3[100];

    private static PlatformToolEditorWindow window;

    [MenuItem("Window/PlatformTool")]
    static void CreateWindow()
    {
        window = EditorWindow.GetWindow<PlatformToolEditorWindow>(false, "Platform Generator", true);
        window.minSize = new Vector2(200, 200);
        window.ShowUtility();
    }
    private void OnDestroy()
    {
        OnDisable();
    }
    private void OnDisable()
    {
        if (startPoint)
            DestroyImmediate(startPoint.gameObject);
        if (p1Point)
            DestroyImmediate(p1Point.gameObject);
        if (p2Point)
            DestroyImmediate(p2Point.gameObject);
        if (endPoint)
            DestroyImmediate(endPoint.gameObject);
        if (lines != null)
            for (int i = 0; i < lines.Length; i++)
                DestroyImmediate(lines[i].gameObject);
        lines = null;
        if (lanes != null)
            for (int i = 0; i < lanes.Length; i++)
            {
                DestroyImmediate(lanes[i].LocalCurve);
                DestroyImmediate(lanes[i]);
            }
        lanes = null;
        if (platformRoot)
            DestroyImmediate(platformRoot);
    }
    private void Awake()
    {
        OnEnable();
    }
    private void OnEnable()
    {
        if (!startPoint)
        {
            startPoint = new GameObject().transform;
            startPoint.gameObject.name = "Start Point";
            startPoint.gameObject.hideFlags = HideFlags.DontSave;
        }
        if (!p1Point)
        {
            p1Point = new GameObject().transform;
            p1Point.gameObject.name = "P1 Point";
            p1Point.gameObject.hideFlags = HideFlags.DontSave;
        }
        if (!p2Point)
        {
            p2Point = new GameObject().transform;
            p2Point.gameObject.name = "P2 Point";
            p2Point.gameObject.hideFlags = HideFlags.DontSave;
        }
        if (!endPoint)
        {
            endPoint = new GameObject().transform;
            endPoint.gameObject.name = "End Point";
            endPoint.gameObject.hideFlags = HideFlags.DontSave;
        }
    }
    private void OnGUI()
    {
        using (new GUILayout.HorizontalScope())
        {
            GUILayout.FlexibleSpace();
            GUILayout.Label("Platform Generator");
            GUILayout.FlexibleSpace();
        }

        EditorGUILayout.Space();

        MeshFilters = EditorGUILayout.ObjectField("Mesh filters container", MeshFilters, typeof(SOLinkedListMeshFilterContainer), false) as SOLinkedListMeshFilterContainer;
        Terrains = EditorGUILayout.ObjectField("Terrains container", Terrains, typeof(SOLinkedListTerrainContainer), false) as SOLinkedListTerrainContainer;
        PlatformLayer = EditorGUILayout.ObjectField("Platform Layer", PlatformLayer, typeof(LayerHolder), false) as LayerHolder;
        SidePlatformLayer = EditorGUILayout.ObjectField("Side Platform Layer", SidePlatformLayer, typeof(LayerHolder), false) as LayerHolder;
        DefaultMaterial = EditorGUILayout.ObjectField("Platform renderer material", DefaultMaterial, typeof(Material), false) as Material;
        FolderTarget = EditorGUILayout.TextField("Platform folder in Assets", FolderTarget);

        EditorGUILayout.Space();

        if (!MeshFilters || !Terrains || !PlatformLayer || !SidePlatformLayer || !DefaultMaterial || FolderTarget == null)
        {
            EditorGUILayout.HelpBox("Not all necessary fields are setted correctly", MessageType.Error);
            return;
        }

        PlatformSize = EditorGUILayout.Vector2Field("Platform size", PlatformSize);
        StepSize = EditorGUILayout.Vector2Field("Platform step size", StepSize);
        SidePlatformSize = EditorGUILayout.FloatField("Side Platform size", SidePlatformSize);

        EditorGUILayout.Space();

        ValidPoints = Mathf.Clamp(EditorGUILayout.IntField("Curve valid points", ValidPoints), V3BezierCurve.MinValidPoints, V3BezierCurve.MaxValidPoints);
        MeshPrecision = Mathf.Clamp(EditorGUILayout.FloatField("Precision (bigger value less vertices)", MeshPrecision), 0.001f, 100f);
        LaneDistance = Mathf.Clamp(EditorGUILayout.FloatField("Distance between lanes", LaneDistance), 1f, 100f);
        LaneCount = Mathf.Clamp(EditorGUILayout.IntField("Number of lanes", LaneCount), 1, 20);
        ShowLines = EditorGUILayout.Toggle("Show lane lines", ShowLines);

        if (lanes == null || lanes.Length != LaneCount)
        {
            if (lanes != null)
                for (int i = 0; i < lanes.Length; i++)
                {
                    ScriptableObject.DestroyImmediate(lanes[i].LocalCurve);
                    ScriptableObject.DestroyImmediate(lanes[i]);
                }

            lanes = new Lane[LaneCount];
            for (int i = 0; i < lanes.Length; i++)
            {
                Lane lane = ScriptableObject.CreateInstance<Lane>();
                lane.name = "Lane" + i;
                lane.LocalCurve = ScriptableObject.CreateInstance<V3BezierCurve>();
                lane.LocalCurve.name = "Curve" + i;
                lanes[i] = lane;
            }
        }

        if (ShowLines)
        {
            if (lines == null || lines.Length != LaneCount)
            {
                if (lines != null)
                    for (int i = 0; i < lines.Length; i++)
                        DestroyImmediate(lines[i].gameObject);

                lines = new LineRenderer[LaneCount];
                for (int i = 0; i < lines.Length; i++)
                {
                    lines[i] = new GameObject().AddComponent<LineRenderer>();
                    lines[i].gameObject.hideFlags = HideFlags.HideAndDontSave;
                }
            }
        }
        else
        {
            if (lines != null)
                for (int i = 0; i < lines.Length; i++)
                    DestroyImmediate(lines[i].gameObject);
            lines = null;
        }

        int midLane = lanes.Length / 2;

        for (int i = 0; i < lanes.Length; i++)
        {
            float distanceToCenter = i == midLane ? 0.0000000f : (i - midLane) * LaneDistance;
            Lane ln = lanes[i];
            ln.LocalCurve.Set(startPoint.position + startPoint.right * distanceToCenter, p1Point.position + p1Point.right * distanceToCenter, p2Point.position + p2Point.right * distanceToCenter, endPoint.position + endPoint.right * distanceToCenter, ValidPoints);
            ln.LocalCurve.ForceUpdateLenghts();
            ln.OnValidate();

            if (ShowLines)
            {
                LineRenderer line = lines[i];
                points[0] = ln.StartLocalPosition;
                points[99] = ln.EndLocalPosition;
                float intervall = 1f / 99f;
                for (int j = 1; j < points.Length - 1; j++)
                {
                    points[i] = ValidPoints == 2 ? ln.LocalCurve.CalculateBezierFirst(intervall * i) : (ValidPoints == 3 ? ln.LocalCurve.CalculateQuadraticBezier(intervall * i) : ln.LocalCurve.CalculateCubicBezier(intervall * i));
                }
                line.SetPositions(points);
            }
        }

        if (lanes[midLane].LocalCurve.Length <= 0f)
            return;

        using (new GUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Generate platform"))
            {
                if (platformRoot)
                    DestroyImmediate(platformRoot);

                Tuple<Mesh, Mesh> terrains = GenerateMeshes(lanes[midLane]);

                platformRoot = SpawnPlatAndGetRoot(terrains.Item1, terrains.Item2, lanes, startPoint.up, endPoint.up);
            }

            if (platformRoot && GUILayout.Button("Save generated platform"))
            {
                string folderPath = "Assets/";
                folderPath = Directory.Exists(Path.Combine(folderPath, "ScriptableObjects")) ? Path.Combine(folderPath, "ScriptableObjects") : AssetDatabase.GUIDToAssetPath(AssetDatabase.CreateFolder("Assets", "ScriptableObjects"));
                folderPath = Directory.Exists(Path.Combine(folderPath, "Levels")) ? Path.Combine(folderPath, "Levels") : AssetDatabase.GUIDToAssetPath(AssetDatabase.CreateFolder(folderPath, "Levels"));
                if (FolderTarget.Length > 0)
                    folderPath = Directory.Exists(Path.Combine(folderPath, FolderTarget)) ? Path.Combine(folderPath, FolderTarget) : AssetDatabase.GUIDToAssetPath(AssetDatabase.CreateFolder(folderPath, FolderTarget));

                string meshesPath = Directory.Exists(Path.Combine(folderPath, "Meshes")) ? Path.Combine(folderPath, "Meshes") : AssetDatabase.GUIDToAssetPath(AssetDatabase.CreateFolder(folderPath, "Meshes"));

                MeshFilter[] filters = platformRoot.GetComponentsInChildren<MeshFilter>(true);
                for (int i = 0; i < filters.Length; i++)
                {
                    //AssetDatabase.CreateAsset(filters[i].mesh, Path.Combine(meshesPath, "Mesh" + i));
                    AssetDatabase.CreateAsset(filters[i].sharedMesh, meshesPath + "Mesh" + i + ".asset");
                }

                string laneFolder = Directory.Exists(Path.Combine(folderPath, "Lanes")) ? Path.Combine(folderPath, "Lanes") : AssetDatabase.GUIDToAssetPath(AssetDatabase.CreateFolder(folderPath, "Lanes"));

                for (int i = 0; i < lanes.Length; i++)
                {
                    Lane current = lanes[i];
                    //AssetDatabase.CreateAsset(current.LocalCurve, Path.Combine(laneFolder, current.LocalCurve.name + ".asset"));
                    //AssetDatabase.CreateAsset(current, Path.Combine(laneFolder, current.name + ".asset"));
                    AssetDatabase.CreateAsset(current.LocalCurve, laneFolder + current.LocalCurve.name + ".asset");
                    AssetDatabase.CreateAsset(current, laneFolder + current.name + ".asset");
                }

                platformRoot.name = "Platform";
                GameObject go = PrefabUtility.CreatePrefab(folderPath + "Platform.prefab", platformRoot);
                SOPool pool = ScriptableObject.CreateInstance<SOPool>();
                pool.name = "Pool";
                pool.Prefab = go;
                //AssetDatabase.CreateAsset(pool, Path.Combine(folderPath, "Pool"));
                AssetDatabase.CreateAsset(pool, folderPath + "Pool.asset");

                lanes = null;

                OnDisable();
                OnEnable();
            }
        }
    }

    #region Generation
    private Tuple<Mesh, Mesh> GenerateMeshes(Lane centralLane)
    {
        Mesh meshTerrain = new Mesh();
        meshTerrain.Clear();

        Mesh meshSideTerrain = new Mesh();
        meshSideTerrain.Clear();

        Tuple<Mesh, Mesh> res = new Tuple<Mesh, Mesh>();
        res.Item1 = meshTerrain;
        res.Item2 = meshSideTerrain;


        List<Vector3> verticesTerrain = new List<Vector3>();
        List<Vector2> uvsTerrain = new List<Vector2>();
        List<int> trianglesTerrain = new List<int>();
        List<Vector3> verticesSideTerrain = new List<Vector3>();
        List<Vector2> uvsSideTerrain = new List<Vector2>();
        List<int> trianglesSideTerrain = new List<int>();

        float percentageIncrement = MeshPrecision * centralLane.LocalCurve.InverseLength;
        float current = percentageIncrement;
        bool exit = false;
        Transform waypoint = new GameObject().transform;
        waypoint.gameObject.hideFlags = HideFlags.HideAndDontSave;

        #region TerrainGeneration
        //Platform plat = ToEdit.GetComponent<Platform>();
        //if (plat == null)
        //    plat = ToEdit.gameObject.AddComponent<Platform>();

        //plat.SetPlatform(GetFirstAvailableId(), start, p1, p2, end, terrain, NewMeshValidPoints);

        RectCorners lastRect = new RectCorners();
        RectCorners nextRect = new RectCorners();
        LineCorners lastLine = new LineCorners();
        LineCorners nextLine = new LineCorners();
        Vector3 lastCenter = centralLane.StartLocalPosition;
        Vector3 lastUp = startPoint.up;
        Vector3 a = Vector3.zero;
        Vector3 b = Vector3.zero;
        Vector3 dir = centralLane.StartLocalDirection;
        //Vector3 dir = (a - b).normalized;
        waypoint.position = lastCenter;
        waypoint.LookAt(dir + lastCenter, lastUp);
        Vector3 lastForward = waypoint.forward;
        //terrain.localPosition = Vector3.zero;
        //terrain.rotation = Quaternion.identity;

        Vector3 lastRight = waypoint.right;

        lastLine.GenerateNewLineCorners(lastCenter, PlatformSize.x, lastUp, lastRight);
        lastRect.GenerateNewRectCorners(lastCenter, PlatformSize, lastUp, lastRight);

        lastRect.GenerateMeshV(true, verticesTerrain);

        while (!exit)
        {
            Vector3 center;
            Vector3 right;
            Vector3 up;
            Vector3 forward;
            a = ValidPoints == 2 ? centralLane.LocalCurve.CalculateBezierFirst(current + 0.00001f) : (ValidPoints == 3 ? centralLane.LocalCurve.CalculateQuadraticBezier(current + 0.00001f) : centralLane.LocalCurve.CalculateCubicBezier(current + 0.00001f));
            b = ValidPoints == 2 ? centralLane.LocalCurve.CalculateBezierFirst(current - 0.00001f) : (ValidPoints == 3 ? centralLane.LocalCurve.CalculateQuadraticBezier(current - 0.00001f) : centralLane.LocalCurve.CalculateCubicBezier(current - 0.00001f));
            dir = (a - b).normalized;
            if (current >= 1f)
            {
                //end.rotation = start.rotation;
                up = endPoint.up;
                exit = true;
                center = centralLane.EndLocalPosition;
                dir = centralLane.EndLocalDirection;
                waypoint.position = center;
                waypoint.LookAt(dir + center, up);
                forward = waypoint.forward;
                right = waypoint.right;
                //end.rotation = waypoint.rotation;
            }
            else
            {
                up = Vector3.Lerp(startPoint.up, endPoint.up, current);
                center = ValidPoints == 2 ? centralLane.LocalCurve.CalculateBezierFirst(current) : (ValidPoints == 3 ? centralLane.LocalCurve.CalculateQuadraticBezier(current) : centralLane.LocalCurve.CalculateCubicBezier(current));
                waypoint.position = center;
                waypoint.LookAt(dir + center, up);
                forward = waypoint.forward;
                right = waypoint.right;
            }

            nextLine.GenerateNewLineCorners(center, PlatformSize.x, up, right);
            nextRect.GenerateNewRectCorners(center, PlatformSize, up, right);

            if (Mathf.Approximately(1f, Vector3.Dot(right, lastRight)) && Mathf.Approximately(1f, Vector3.Dot(up, lastUp)) && Mathf.Approximately(1f, Vector3.Dot(forward, lastForward)) && !exit)
            {
                current += percentageIncrement;
                continue;
            }

            lastLine.GenerateConnectionMeshV(nextLine, StepSize, SidePlatformSize, verticesSideTerrain);
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

        return res;
    }
    private GameObject SpawnPlatAndGetRoot(Mesh mesh, Mesh sideMesh, Lane[] lanes, Vector3 startUp, Vector3 endUp)
    {
        GameObject res = new GameObject();
        Platform plat = res.AddComponent<Platform>();
        plat.Lanes = new List<Lane>(lanes).ToArray();
        plat.StartLocalUp = startUp;
        plat.EndLocalUp = endUp;
        Rigidbody bd = res.AddComponent<Rigidbody>();
        bd.isKinematic = true;
        bd.useGravity = false;
        res.layer = PlatformLayer;

        GameObject child = new GameObject();
        child.transform.parent = res.transform;
        child.layer = PlatformLayer;
        child.AddComponent<MeshFilter>().mesh = mesh;
        child.AddComponent<MeshRenderer>().material = DefaultMaterial;
        child.AddComponent<BoxCollider>().isTrigger = true;
        child.name = "Terrain";
        NavMeshSourceTag tag = child.AddComponent<NavMeshSourceTag>();
        tag.M_Meshes = MeshFilters;
        tag.M_Terrains = Terrains;
        tag.Filter = child.GetComponent<MeshFilter>();

        child = new GameObject();
        child.transform.parent = res.transform;
        child.layer = SidePlatformLayer;
        child.AddComponent<MeshFilter>().mesh = sideMesh;
        child.AddComponent<MeshRenderer>().material = DefaultMaterial;
        child.AddComponent<BoxCollider>().isTrigger = true;
        child.name = "SideTerrain";
        tag = child.AddComponent<NavMeshSourceTag>();
        tag.M_Meshes = MeshFilters;
        tag.M_Terrains = Terrains;
        tag.Filter = child.GetComponent<MeshFilter>();
        return res;
    }
    #endregion

    #region UtilityClasses
    [SerializeField]
    public class LineCorners
    {
        public Vector3 Left;
        public Vector3 Right;
        public Vector3 UpDir;
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
    [SerializeField]
    public class RectCorners
    {
        public Vector3 UpLeft;
        public Vector3 UpRight;
        public Vector3 DownLeft;
        public Vector3 DownRight;
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
    #endregion
}
