using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;

    //X = 4   Y = -7   (buoni settings)
    public Vector3 CameraArm;

    private void LateUpdate()
    {
        transform.position = Target.position + CameraArm;
        //transform.LookAt(Target);
    }
}
