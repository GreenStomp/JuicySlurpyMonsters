using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Character_Type
{
    Default,
    Red,
    Purple,
    Yell,
    Green,
}

public class CharacterInfo : MonoBehaviour
{
    public string Name;

    public Character_Type Type;

    public string Description_Lore;

    public Sprite Icon;

    public bool IsUnlocked;

    //public Skill Skill_Passive

    // dentro skill avro:
    //Skill.Name   --- Skill.Description   ---- Skill.Icon

    //public Skill Skill_Active
    //public Skill Skill_Ultimate
}
