using UnityEngine;
using SOPRO;
[CreateAssetMenu(fileName = "RevertControls", menuName = "Level/Platform/Special/RevertControls")]
public class RevertControls : SpecialPlatform
{
    public MobileInput Input;
    public LayerHolder MonsterLayer;
    public override void OnEnter(Transform entered, float currentPercentage)
    {
        if (entered.gameObject.layer == MonsterLayer.LayerIndex)
            this.Input.InvertedControls = !this.Input.InvertedControls;
    }

    public override void OnExit(Transform exited, float prevPercentage)
    {

    }

    public override void OnStepTaken(Transform walker, float currentPercentage, float prevPercentage)
    {

    }
}