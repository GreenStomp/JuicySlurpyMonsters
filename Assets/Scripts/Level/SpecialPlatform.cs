using UnityEngine;
public abstract class SpecialPlatform : ScriptableObject
{
    public abstract void OnEnter(Transform entered , float currentPercentage);
    public abstract void OnExit(Transform exited, float prevPrecentage);
    public abstract void OnStepTaken(Transform walker, float currentPercentage, float prevPercentage);
}