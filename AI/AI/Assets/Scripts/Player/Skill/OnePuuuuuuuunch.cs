using UnityEngine;
public class OnePuuuuuuuunch : Skill
{
    public const string SkillName = "Heavy Punch";
    public const string SkillInfo = "Onepuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuunch";
    public const SkillType Type = SkillType.Active | SkillType.Offensive;

    public string AnimatorTriggerName;

    private int AnimatorTriggerHash;

    protected override void SetSkillInfo(SkillInfo info)
    {
        info.Set(SkillName, SkillInfo, CooldownTime, iconPrefabPath, Type);
    }
    protected override void Update()
    {
        base.Update();
        if (IsCooldownOver && MobileInput.Instance.DoubleTap)
        {
            if (Controller.Animator != null)
                Controller.Animator.SetTrigger(AnimatorTriggerHash);
            CooldownTimer = CooldownTime;
        }
    }
    void Start()
    {
        AnimatorTriggerHash = Animator.StringToHash(AnimatorTriggerName);
    }
}