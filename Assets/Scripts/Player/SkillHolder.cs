using UnityEngine;
[RequireComponent(typeof(EntityStats))]
public abstract class SkillHolder : MonoBehaviour
{
    public Transform Owner { get; private set; }
    //public Animator Animator { get; private set; }
    public EntityStats Stats { get; private set; }
    public abstract bool AreSkillsUsable { get; }

    protected virtual void Awake()
    {
        Owner = this.transform;
        //Animator = GetComponent<Animator>();
        Stats = GetComponent<EntityStats>();
    }
}