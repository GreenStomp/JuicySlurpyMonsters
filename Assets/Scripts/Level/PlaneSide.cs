using UnityEngine;
[CreateAssetMenu(fileName = "PlaneSide", menuName = "Level/PlaneSide")]
public class PlaneSide : ScriptableObject
{
    public Plane Plane = new Plane();
    public Vector3 Point = new Vector3(0f, 0f, 0f);
}