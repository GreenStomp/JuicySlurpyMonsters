using UnityEngine;
public class Punch : Skill
{
    public const string SkillName = "Punch";
    public const string SkillInfo = "Normal left punch with nonsensical destructive power";
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
        if (IsCooldownOver && MobileInput.Instance.Tap)
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