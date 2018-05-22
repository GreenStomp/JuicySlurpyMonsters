using UnityEngine;
public interface IController
{
    Transform Owner { get; }
    Animator Animator { get; }
    bool AreSkillsUsable();
}