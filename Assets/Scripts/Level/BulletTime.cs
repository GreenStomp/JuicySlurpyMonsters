using UnityEngine;
public class BulletTime : SpecialPlatform
{
    public float TimeScale = 0.5f;
    private float prevTimescale;
    public override void OnEnter(PlatformManager.Step step)
    {
        if (step.Owner.gameObject.layer == GameManager.MonsterLayer)
        {
            prevTimescale = Time.timeScale;
            Time.timeScale = TimeScale;
        }
    }

    public override void OnExit(PlatformManager.Step step)
    {
        if (step.Owner.gameObject.layer == GameManager.MonsterLayer)
            Time.timeScale = prevTimescale;
    }

    public override void OnStepTaken(PlatformManager.Step step, float prevPercentage)
    {
        if (step.Owner.gameObject.layer == GameManager.MonsterLayer)
            Time.timeScale = Utils.Lerp(TimeScale, prevTimescale, step.Percentage);
    }
}