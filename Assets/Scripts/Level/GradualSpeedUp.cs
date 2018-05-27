using UnityEngine;
[CreateAssetMenu(fileName = "GradualSpeedUp", menuName = "Level/Platform/Special/GradualSpeedUp")]
public class GradualSpeedUp : SpecialPlatform
{
    public float SpeedIncrement = 20f;
    public override void OnEnter(Transform entered, float currentPercentage)
    {
    }

    public override void OnExit(Transform exited, float prevPercentage)
    {
        EntityStats user = exited.GetComponent<EntityStats>();
        if (user != null)
            user.Speed += (1f - prevPercentage) * SpeedIncrement;
    }

    public override void OnStepTaken(Transform walker, float currentPercentage, float prevPercentage)
    {
        EntityStats user = walker.GetComponent<EntityStats>();
        if (user != null)
            user.Speed += (currentPercentage - prevPercentage) * SpeedIncrement;
    }
}