using UnityEngine;
using SOPRO.Variables;
[CreateAssetMenu(fileName = "Lane", menuName = "Level/Platform/Lane")]
public class Lane : ScriptableObject
{
    public V3BezierCurve LocalCurve;
    public SOVariableVector3 StartLocalDirection;
    public SOVariableVector3 StartLocalPosition;
    public SOVariableVector3 EndLocalDirection;
    public SOVariableVector3 EndLocalPosition;

#if UNITY_EDITOR
    [Range(0.0000001f, 0.01f)]
    public float EDITOR_Precision = 0.00001f;
    void OnValidate()
    {
        if (!LocalCurve)
            return;

        Vector3 dir = (LocalCurve.GetPoint(EDITOR_Precision) - LocalCurve.GetPoint(-EDITOR_Precision)).normalized;
        if (StartLocalDirection)
            StartLocalDirection.Value = dir;

        dir = (LocalCurve.GetPoint(1f + EDITOR_Precision) - LocalCurve.GetPoint(1f - EDITOR_Precision)).normalized;
        if (EndLocalDirection)
            EndLocalDirection.Value = dir;

        if (EndLocalPosition)
            EndLocalPosition.Value = LocalCurve.GetPoint(1f);
        if (StartLocalPosition)
            StartLocalPosition.Value = LocalCurve.GetPoint(0);
    }
#endif
}