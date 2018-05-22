using UnityEngine;
public class SkillInfo
{
    public string Name { get; private set; }
    public string GeneralInfo { get; private set; }
    public float CoolDownTime { get; private set; }
    public Sprite Icon { get; private set; }
    public SkillType Type { get; private set; }
    public SkillInfo()
    {

    }
    public void Set(string name, string generalInfo, float coolDownTime, Sprite icon, SkillType type)
    {
        this.Name = name;
        this.GeneralInfo = generalInfo;
        this.CoolDownTime = coolDownTime;
        this.Icon = icon;
        this.Type = type;
    }
}