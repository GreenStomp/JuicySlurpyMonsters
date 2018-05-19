/*using UnityEngine;
public class MoveBeforeTarget : MonoBehaviour
{
    public EntityStats Target;
    public float InitialBoostedMovement;
    private PlatformManager.Step step;
    void Start()
    {
        step = new PlatformManager.Step(this.transform);
        step.CalculateNextStep(InitialBoostedMovement);
        this.transform.LookAt(step.TangentToCenter + this.transform.position, step.Up);
        this.transform.position = step.Center;
    }
    void Update()
    {
        step.CalculateNextStep(Target.Speed * Time.deltaTime);
        this.transform.LookAt(step.TangentToCenter + this.transform.position, step.Up);
        this.transform.position = step.Center;
    }
}*/