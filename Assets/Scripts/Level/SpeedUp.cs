public class SpeedUp : SpecialPlatform
{
    public float SpeedMultiplier = 2f;
    public override void OnEnter(PlatformManager.Step step)
    {
        EntityStats user = step.Owner.GetComponent<EntityStats>();
        if (user != null)
            user.Speed *= SpeedMultiplier;
    }
    public override void OnExit(PlatformManager.Step step)
    {
    }
    public override void OnStepTaken(PlatformManager.Step step, float prevPercentage)
    {
    }
}