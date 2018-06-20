using UnityEngine;
public class PlaneUpdater : MonoBehaviour
{
    [SerializeField]
    private PlaneSide planeSide;
    [SerializeField]
    private Transform myTransform;

    void Update()
    {
        Vector3 forward = myTransform.forward;
        Vector3 p = myTransform.position;
        planeSide.Plane = new Plane(forward, p);
        planeSide.Point = p - forward;
    }
}