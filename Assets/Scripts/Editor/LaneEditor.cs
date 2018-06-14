using UnityEngine;
using System.Reflection;
using UnityEditor;
using System.IO;
[CustomEditor(typeof(Lane))]
public class LaneEditor : Editor
{
    //private MethodInfo onValidate = typeof(Lane).GetMethod("OnValidate", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
    private Lane lane;
    private string path;
    void OnEnable()
    {
        lane = target as Lane;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        using (new GUILayout.HorizontalScope())
        {
            if (/*onValidate != null && */GUILayout.Button("Update values"))
                lane.OnValidate();
                //onValidate.Invoke(lane, new object[0]);

            if (GUILayout.Button("Create curve instance"))
            {
                V3BezierCurve curve = ScriptableObject.CreateInstance<V3BezierCurve>();
                lane.LocalCurve = curve;

                curve.name = lane.name + "Curve";

                path = Path.GetDirectoryName(AssetDatabase.GetAssetPath(lane));
                path = Path.Combine(path, curve.name);
                path = Path.ChangeExtension(path, ".asset");

                AssetDatabase.CreateAsset(curve, path);
                AssetDatabase.Refresh();
            }
        }

        if (!Mathf.Approximately(1f, lane.EndLocalDirection.magnitude))
            EditorGUILayout.HelpBox("The end direction is not set correctly, using this lane may lead to errors", MessageType.Error);

        if (!Mathf.Approximately(1f, lane.StartLocalDirection.magnitude))
            EditorGUILayout.HelpBox("The start direction is not set correctly, using this lane may lead to errors", MessageType.Error);
    }
}
