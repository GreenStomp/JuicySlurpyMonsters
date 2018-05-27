using UnityEngine;
[CreateAssetMenu(fileName = "GradualSpeedDown", menuName = "Level/Platform/Special/GradualSpeedDown")]
public class GradualSpeedDown : SpecialPlatform
{
    public float SpeedDecrement = 20f;
    public override void OnEnter(Transform entered, float currentPercentage)
    {
    }
    public override void OnExit(Transform exited, float prevPrecentage)
    {
        EntityStats user = exited.GetComponent<EntityStats>();
        if (user != null)
            user.Speed -= (1f - prevPrecentage) * SpeedDecrement;
    }
    public override void OnStepTaken(Transform walker, float currentPercentage, float prevPercentage)
    {
        EntityStats user = walker.GetComponent<EntityStats>();
        if (user != null)
            user.Speed -= (currentPercentage - prevPercentage) * SpeedDecrement;
    }
}