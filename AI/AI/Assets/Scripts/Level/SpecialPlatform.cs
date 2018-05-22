using UnityEngine;
[RequireComponent(typeof(Platform))]
public abstract class SpecialPlatform : MonoBehaviour
{
    protected Platform OwnPlat { get; private set; }
    public abstract void OnEnter(PlatformManager.Step step);
    public abstract void OnExit(PlatformManager.Step step);
    public abstract void OnStepTaken(PlatformManager.Step step, float prevPercentage);
    protected virtual void Awake()
    {
        OwnPlat = GetComponent<Platform>();
    }
}