using UnityEngine;
using SOPRO.Variables;
public class MonoBehaviourVIUnoPuntoZero : MonoBehaviour {

    public SOVariablePlane Plane;
    public SOVariableVector3 Point;
    Transform myTransform;
    private void Awake()
    {
        myTransform = transform;
        Update();
    }
    void Update ()
    {
        Vector3 forward = myTransform.forward;
        Vector3 p = myTransform.position;
        Plane.Value = new Plane(forward, p);
        Point.Value = p - forward;
	}
}