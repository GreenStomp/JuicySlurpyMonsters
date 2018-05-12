public class GradualSpeedUp : SpecialPlatform
{
    public float SpeedIncrement = 20f;
    public override void OnEnter(PlatformManager.Step step)
    {
    }
    public override void OnExit(PlatformManager.Step step)
    {
        EntityStats user = step.Owner.GetComponent<EntityStats>();
        if (user != null)
            user.Speed += (1f - step.Percentage) * SpeedIncrement;
    }
    public override void OnStepTaken(PlatformManager.Step step, float prevPercentage)
    {
        EntityStats user = step.Owner.GetComponent<EntityStats>();
        if (user != null)
            user.Speed += (step.Percentage - prevPercentage) * SpeedIncrement;
    }
}