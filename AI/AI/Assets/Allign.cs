using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Allign : MonoBehaviour
{
    public float Distance;
    public float Radius;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 0.5f, -(transform.up), out hit, Distance))
        {
            transform.rotation = Quaternion.LookRotation(Vector3.Cross(transform.right, hit.normal));
        }
    }
}
