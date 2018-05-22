using UnityEngine;
[RequireComponent(typeof(IStats), typeof(IController))]
public abstract class Skill : MonoBehaviour
{
    /// <summary>
    /// Determines if the skill is currently on cooldown
    /// </summary>
    public bool IsCooldownOver { get { return Mathf.Approximately(cooldownTimer, 0f); } }
    /// <summary>
    /// Returns a normalized (0 , 1) value indicating how much cooldown time is left. 0 = cooldown is over. 1 = there is CooldownTime time left
    /// </summary>
    public float NormalizedCooldownTimeLeft { get { return cooldownTimer * inverseCooldownTime; } }
    /// <summary>
    /// Time required for the skill before a new usage
    /// </summary>
    public float CooldownTime { get { return cooldownTime; } }
    /// <summary>
    /// Timer that indicates how much time is left before cooldown is over. 0 = cooldown over. Maxed out to CooldownTime value
    /// </summary>
    protected float CooldownTimer { get { return cooldownTimer; } set { cooldownTimer = Mathf.Clamp(value, 0f, cooldownTime); } }
    /// <summary>
    /// Object that stores and manages entity basic stats values
    /// </summary>
    protected IStats Stats { get; private set; }
    /// <summary>
    /// Object that manages entity basic logic
    /// </summary>
    protected IController Controller { get; private set; }

    [SerializeField]
    protected Sprite iconPrefabPath;
    [SerializeField]
    private float cooldownTime;
    [SerializeField]
    private float inverseCooldownTime;

    private float cooldownTimer;

    /// <summary>
    /// Creates and retuns info about the skill through SkillInfo class. Dispose properly of its fields once you don't need it anymore
    /// </summary>
    /// <returns>SkillInfo class</returns>
    public SkillInfo GetSkillInfo()
    {
        SkillInfo info = new SkillInfo();
        SetSkillInfo(info);
        return info;
    }
    /// <summary>
    /// Sets the given skillinfo fields to correct values
    /// </summary>
    /// <param name="info">instance to set correctly</param>
    protected abstract void SetSkillInfo(SkillInfo info);

    protected virtual void Update()
    {
        if (!IsCooldownOver)
        {
            cooldownTimer = Mathf.Max(cooldownTimer - Time.deltaTime, 0f);
        }
    }
    protected virtual void Awake()
    {
        Stats = GetComponent<IStats>();
        Controller = GetComponent<IController>();
        cooldownTimer = cooldownTime;
    }
    protected virtual void OnValidate()
    {
        inverseCooldownTime = Mathf.Approximately(0f, cooldownTime) ? 0f : 1f / cooldownTime;
    }
}