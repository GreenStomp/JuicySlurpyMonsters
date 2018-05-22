using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLevel : MonoBehaviour
{
    public float Speed;
    PlatformManager.Step step;
    private void Start()
    {
        step = new PlatformManager.Step(this.transform);
    }
    private void Update()
    {
        step.CalculateNextStep(Speed * Time.deltaTime);
        transform.LookAt(step.TangentToCenter + this.transform.position, step.Up);

        transform.position = step.Center;
    }
}
