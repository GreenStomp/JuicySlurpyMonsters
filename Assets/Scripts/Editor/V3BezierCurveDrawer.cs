using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(V3BezierCurve))]
public class V3BezierCurveDrawer : UnityEditor.Editor
{
    V3BezierCurve curve;
    private void OnEnable()
    {
        curve = target as V3BezierCurve;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Update Lengths"))
            curve.ForceUpdateLenghts();
    }
}