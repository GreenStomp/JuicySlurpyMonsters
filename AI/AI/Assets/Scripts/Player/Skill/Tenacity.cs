using UnityEngine;
public class Tenacity : Skill
{
    public const string SkillName = "Tenacity";
    public const string SkillInfo = "Gives a passive chance to recover 1 health when damaged";
    public const SkillType Type = SkillType.Special | SkillType.Defensive;

    public float RecoverChance { get { return recoverChance; } set { recoverChance = Mathf.Clamp01(value); } }
    public int RecoverAmount { get { return recoverAmount; } set { recoverAmount = Mathf.Max(0, value); } }

    private float recoverChance = 0.15f;
    private int recoverAmount = 1;

    private int prevHealth;

    protected override void SetSkillInfo(SkillInfo info)
    {
        info.Set(SkillName, SkillInfo, CooldownTime, iconPrefabPath, Type);
    }
    protected override void Update()
    {
        base.Update();
        if (prevHealth > Stats.Health && Random.Range(0f, 1f) <= recoverChance)
        {
            Stats.Health += recoverAmount;
        }
        prevHealth = Stats.Health;
    }
    private void Start()
    {
        prevHealth = Stats.Health;
    }
}