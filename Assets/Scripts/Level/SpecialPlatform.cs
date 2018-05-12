using UnityEngine;
public abstract class SpecialPlatform : MonoBehaviour
{
    public abstract void OnEnter(PlatformManager.Step step);
    public abstract void OnExit(PlatformManager.Step step);
    public abstract void OnStepTaken(PlatformManager.Step step, float prevPercentage);
}