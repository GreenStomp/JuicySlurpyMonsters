using UnityEngine;
[CreateAssetMenu(fileName = "SpeedUp" , menuName = "Level/Platform/Special/SpeedUp")]
public class SpeedUp : SpecialPlatform
{
    public float SpeedMultiplier = 2f;
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