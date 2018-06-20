using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(SimpleBezierCurve))]
public class FloatBezierCurveDrawer : UnityEditor.Editor
{
    SimpleBezierCurve curve;
    private void OnEnable()
    {
        curve = target as SimpleBezierCurve;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Update Lengths"))
            curve.ForceUpdateLenghts();
    }
}