using UnityEngine;
[CreateAssetMenu(fileName = "SpeedDown", menuName = "Level/Platform/Special/SpeedDown")]
public class SpeedDown : SpecialPlatform
{
    public float SpeedMultiplier = 0.5f;
    public override void OnEnter(Transform entered, float currentPercentage)
    {
        EntityStats user = entered.GetComponent<EntityStats>();
        if (user != null)
            user.Speed *= SpeedMultiplier;
    }
    public override void OnExit(Transform exited, float prevPercentage)
    {
    }
    public override void OnStepTaken(Transform walker, float currentPercentage, float prevPercentage)
    {
    }
}