using System;
[Flags]
public enum SkillType : byte
{
    None = 0,
    Active = 1,
    Offensive = 1 << 1,
    Defensive = 1 << 2,
    Special = 1 << 3,
    Ultimate = 1 << 4,
    Toggle = 1 << 5,
    SingleUse = 1 << 6,
    Custom = 1 << 7
}