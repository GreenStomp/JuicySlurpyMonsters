using UnityEngine;
[CreateAssetMenu(fileName = "Lane", menuName = "Level/Platform/Lane")]
public class Lane : ScriptableObject
{
    public V3BezierCurve LocalCurve;
    public Vector3 StartLocalDirection;
    public Vector3 StartLocalPosition;
    public Vector3 EndLocalDirection;
    public Vector3 EndLocalPosition;

#if UNITY_EDITOR
    [Range(0.00001f, 0.01f)]
    public float EDITOR_Precision = 0.00001f;
    public void OnValidate()
    {
        if (!LocalCurve)
            return;

        Vector3 dir = (LocalCurve.GetPoint(EDITOR_Precision) - LocalCurve.GetPoint(-EDITOR_Precision)).normalized;
        if (dir.x < EDITOR_Precision)
            dir.x = 0f;
        if (dir.y < EDITOR_Precision)
            dir.y = 0f;
        if (dir.z < EDITOR_Precision)
            dir.z = 0f;
        StartLocalDirection = dir.normalized;

        dir = (LocalCurve.GetPoint(1f + EDITOR_Precision) - LocalCurve.GetPoint(1f - EDITOR_Precision)).normalized;
        if (dir.x < EDITOR_Precision)
            dir.x = 0f;
        if (dir.y < EDITOR_Precision)
            dir.y = 0f;
        if (dir.z < EDITOR_Precision)
            dir.z = 0f;
        EndLocalDirection = dir.normalized;

        EndLocalPosition = LocalCurve.GetPoint(1.00000000000000000000f);
        StartLocalPosition = LocalCurve.GetPoint(0.00000000000000000000f);
    }
#endif
}