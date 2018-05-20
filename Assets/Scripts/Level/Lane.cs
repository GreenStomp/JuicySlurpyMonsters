using UnityEngine;
[CreateAssetMenu(fileName = "Lane", menuName = "Level/Platform/Lane")]
public class Lane : ScriptableObject
{
    public V3BezierCurve LocalCurve;
    public ReferenceVector3 StartLocalDirection;
    public ReferenceVector3 EndLocalDirection;
    public ReferenceVector3 StartLocalUp;
    public ReferenceVector3 EndLocalUp;

#if UNITY_EDITOR
    [Range(0.0000001f, 0.01f)]
    public float EDITOR_Precision = 0.00001f;
    void OnValidate()
    {
        if (!LocalCurve || StartLocalDirection == null || EndLocalDirection == null)
            return;

        Vector3 dir = (LocalCurve.GetPoint(EDITOR_Precision) - LocalCurve.GetPoint(-EDITOR_Precision)).normalized;
        StartLocalDirection.Value = dir;

        dir = (LocalCurve.GetPoint(1f + EDITOR_Precision) - LocalCurve.GetPoint(1f - EDITOR_Precision)).normalized;
        EndLocalDirection.Value = dir;
    }
#endif
}