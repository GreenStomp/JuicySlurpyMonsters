using UnityEngine;
[CreateAssetMenu(fileName = "Lane", menuName = "Level/Platform/Lane")]
public class Lane : ScriptableObject
{
    public V3BezierCurve LocalCurve;
    public Vector3 StartLocalDirection;
    public Vector3 StartLocalPosition;
    public Vector3 EndLocalDirection;
    public Vector3 EndLocalPosition;

//#if UNITY_EDITOR
//    [Range(0.0000001f, 0.01f)]
//    public float EDITOR_Precision = 0.00001f;
//    void OnValidate()
//    {
//        if (!LocalCurve)
//            return;

//        Vector3 dir = (LocalCurve.GetPoint(EDITOR_Precision) - LocalCurve.GetPoint(-EDITOR_Precision)).normalized;
//        StartLocalDirection = dir;

//        dir = (LocalCurve.GetPoint(1f + EDITOR_Precision) - LocalCurve.GetPoint(1f - EDITOR_Precision)).normalized;
//        EndLocalDirection = dir;

//        EndLocalPosition = LocalCurve.GetPoint(1f);
//        StartLocalPosition = LocalCurve.GetPoint(0);
//    }
//#endif
}