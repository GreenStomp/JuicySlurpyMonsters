using UnityEngine;
[RequireComponent(typeof(Animator), typeof(IStats))]
public class PlayerController : MonoBehaviour, IController
{
    public Transform Owner { get; private set; }
    public Animator Animator { get; private set; }
    public float TotalDistanceWalked { get { return step.TotalDistanceWalked; } }
    public uint TotalPlatformsPassed { get { return step.TotalPlatformsPassed; } }
    public uint SpecialPlatformsPassed { get { return step.SpecialPlatformsPassed; } }
    public float BaseSpeed { get { return stats.Speed; } set { stats.Speed = value; } }
    public float SwipeSpeedMultiplier { get; set; }

    public string SpeedParameter = "Speed";
    public string HitParameter = "Hit";
    public string IsDeadParameter = "IsDead";
    public static int SpeedParameterHash { get; private set; }
    public static int HitParameterHash { get; private set; }
    public static int IsDeadParameterHash { get; private set; }

    private delegate State State();

    private PlatformManager.Step step;
    private IStats stats;

    private State current;

    private Vector3 rootMotion;
    private Quaternion rootRotation;
    private Vector3 newPosition;

    private float swipePercentage;
    private float targetPercentage;
    private bool isLeftSwipe;
    void Awake()
    {
        stats = GetComponent<IStats>();
        Owner = this.transform;
        BaseSpeed = 500f;
        SwipeSpeedMultiplier = 10f;
        Animator = GetComponent<Animator>();
    }
    void Start()
    {
        SpeedParameterHash = Animator.StringToHash(SpeedParameter);
        HitParameterHash = Animator.StringToHash(HitParameter);
        IsDeadParameterHash = Animator.StringToHash(IsDeadParameter);
        step = new PlatformManager.Step(this.transform);
        current = Idle;
    }
    State Idle()
    {
        float lanes = step.Current.Lanes / 2;
        if (MobileInput.Instance.SwipeLeft && !Mathf.Approximately(-lanes, swipePercentage))
        {
            isLeftSwipe = true;
            targetPercentage = swipePercentage - 1f;
            return Swiping;
        }
        if (MobileInput.Instance.SwipeRight && !Mathf.Approximately(lanes, swipePercentage))
        {
            isLeftSwipe = false;
            targetPercentage = swipePercentage + 1f;
            return Swiping;
        }

        return Idle;
    }
    State Swiping()
    {
        float sign = isLeftSwipe ? -1f : 1f;

        swipePercentage += sign * Time.deltaTime * SwipeSpeedMultiplier;

        if ((sign < 0f && swipePercentage <= targetPercentage) || (sign > 0f && swipePercentage >= targetPercentage))
        {
            swipePercentage = targetPercentage;
            return Idle;
        }

        return Swiping;
    }
    void Update()
    {
        if (Animator.GetBool(IsDeadParameterHash))
            return;

        step.CalculateNextStep(Time.deltaTime * BaseSpeed);

        float lanes = step.Current.Lanes / 2;
        swipePercentage = Mathf.Clamp(swipePercentage, -lanes, lanes);

        this.transform.LookAt(step.TangentToCenter + this.transform.position, step.Up);

        newPosition = step.Center + transform.right * swipePercentage * step.Current.DistanceBetweenLanes;

        current = current.Invoke();

        Animator.SetFloat(SpeedParameterHash, BaseSpeed);

        transform.position = newPosition;
    }
    void OnAnimatorMove()
    {
        rootMotion = Animator.deltaPosition;
        rootRotation = Animator.deltaRotation;
    }
    public bool AreSkillsUsable()
    {
        return true;
    }
}