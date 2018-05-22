public class RevertControls : SpecialPlatform
{
    public override void OnEnter(PlatformManager.Step step)
    {
        if ((step.Owner.gameObject.layer & GameManager.MonsterLayer) != 0)
            MobileInput.Instance.InvertedControls = !MobileInput.Instance.InvertedControls;
    }
    public override void OnExit(PlatformManager.Step step)
    {
    }
    public override void OnStepTaken(PlatformManager.Step step, float prevPercentage)
    {
    }
}