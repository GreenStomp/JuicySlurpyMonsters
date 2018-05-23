using UnityEngine;
using SOPRO;
[CreateAssetMenu(fileName = "BulletTime", menuName = "Level/Platform/Special/BulletTime")]
public class BulletTime : SpecialPlatform
{
    public float TimeScale = 0.5f;
    public LayerHolder MonsterLayer;

    public bool Activated { get { return activated; } }
    public float InitialTimeScale { get { return initialTimeScale; } }

    private float initialTimeScale = 1f;
    private bool activated = false;

    public void ResetEffect()
    {
        if (activated)
        {
            Time.timeScale = initialTimeScale;
            activated = false;
        }
    }
    public override void OnEnter(Transform entered, float currentPercentage)
    {
        if (activated)
            return;

        if (entered.gameObject.layer == MonsterLayer.LayerIndex)
        {
            initialTimeScale = Time.timeScale;
            Time.timeScale = TimeScale;
            activated = true;
        }
    }

    public override void OnExit(Transform exited, float prevPrecentage)
    {
        if (!activated)
            return;

        if (MonsterLayer.LayerIndex == exited.gameObject.layer)
        {
            Time.timeScale = initialTimeScale;
            activated = false;
        }
    }

    public override void OnStepTaken(Transform walker, float currentPercentage, float prevPercentage)
    {
        if (!activated)
            return;

        if (MonsterLayer.LayerIndex == walker.gameObject.layer)
            Time.timeScale = Utils.Lerp(TimeScale, initialTimeScale, currentPercentage);
    }
}