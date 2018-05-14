using UnityEngine;
public class PlayerController : SkillHolder
{
    public float TotalDistanceWalked { get { return step.TotalDistanceWalked; } }
    public uint TotalPlatformsPassed { get { return step.TotalPlatformsPassed; } }
    public uint SpecialPlatformsPassed { get { return step.SpecialPlatformsPassed; } }

    public float SwipeSpeedMultiplier { get; set; }

    public string BaseLayerName = "Base Layer";
    public string UpperBodyLayerName = "UpperBody";
    public string SpeedParameter = "Speed";
    public string HitParameter = "Hit";
    public string IsDeadParameter = "IsDead";
    public int BaseLayerIndex { get; private set; }
    public int UpperBodyLayerIndex { get; private set; }
    public int SpeedParameterHash { get; private set; }
    public int HitParameterHash { get; private set; }
    public int IsDeadParameterHash { get; private set; }
    public Platform Current { get { return step.Current; } }

    public override bool AreSkillsUsable
    {
        get { return false; }
    }
    private PlatformManager.Step step;

    private IState current;

    //private Vector3 rootMotion;
    //private Quaternion rootRotation;
    private Vector3 newPosition;

    private float swipePercentage;
    private float targetPercentage;
    private bool isLeftSwipe;

    private int prevHealth;
    private bool isDead = false;
    protected override void Awake()
    {
        base.Awake();
        SwipeSpeedMultiplier = 10f;
    }
    protected virtual void Start()
    {
        prevHealth = Stats.Health;

        SpeedParameterHash = Animator.StringToHash(SpeedParameter);
        HitParameterHash = Animator.StringToHash(HitParameter);
        IsDeadParameterHash = Animator.StringToHash(IsDeadParameter);
        //UpperBodyLayerIndex = Animator.GetLayerIndex(UpperBodyLayerName);
        //BaseLayerIndex = Animator.GetLayerIndex(BaseLayerName);

        Swiping swipe = new Swiping(this);
        Idle idle = new Idle(this);

        swipe.Idle = idle;
        idle.Swiping = swipe;

        current = idle;
    }
    void Update()
    {
        if (step == null)
            step = new PlatformManager.Step(this.transform);

        if (isDead)
        {
            //if (Animator.GetAnimatorTransitionInfo(BaseLayerIndex).IsUserName("CloseAnimator"))
            //    Animator.enabled = false;
            return;
        }

        step.CalculateNextStep(Time.deltaTime * Stats.Speed);

        float lanes = step.Current.Lanes / 2;
        swipePercentage = Mathf.Clamp(swipePercentage, -lanes, lanes);

        this.transform.LookAt(step.TangentToCenter + this.transform.position, step.Up);

        newPosition = step.Center + transform.right * swipePercentage * step.Current.DistanceBetweenLanes;

        current = current.OnStateUpdate();

        //Animator.SetFloat(SpeedParameterHash, Stats.Speed);

        transform.position = newPosition;
    }
    void LateUpdate()
    {
        //if (prevHealth != Stats.Health)
        //{
        //    if (Stats.Health <= 0)
        //    {
        //        //Animator.SetTrigger(IsDeadParameterHash);
        //        //Animator.SetFloat(SpeedParameterHash, 0f);
        //        isDead = true;
        //    }
        //    else
        //        Animator.SetTrigger(HitParameterHash);
        //}

        prevHealth = Stats.Health;
    }
    void OnAnimatorMove()
    {
        //rootMotion = Animator.deltaPosition;
        //rootRotation = Animator.deltaRotation;
    }
    private class Swiping : IState
    {
        private PlayerController owner;
        public IState Idle { get; set; }
        public Swiping(PlayerController owner)
        {
            this.owner = owner;
        }
        public IState OnStateUpdate()
        {
            float sign = owner.isLeftSwipe ? -1f : 1f;

            owner.swipePercentage += sign * Time.deltaTime * owner.SwipeSpeedMultiplier;

            if ((sign < 0f && owner.swipePercentage <= owner.targetPercentage) || (sign > 0f && owner.swipePercentage >= owner.targetPercentage))
            {
                owner.swipePercentage = owner.targetPercentage;
                return Idle;
            }

            return this;
        }
    }
    private class Idle : IState
    {
        private PlayerController owner;
        public IState Swiping { get; set; }
        public Idle(PlayerController owner)
        {
            this.owner = owner;
        }
        public IState OnStateUpdate()
        {
            float lanes = owner.step.Current.Lanes / 2;
            if (MobileInput.Instance.SwipeLeft && !Mathf.Approximately(-lanes, owner.swipePercentage))
            {
                owner.isLeftSwipe = true;
                owner.targetPercentage = owner.swipePercentage - 1f;
                return Swiping;
            }
            if (MobileInput.Instance.SwipeRight && !Mathf.Approximately(lanes, owner.swipePercentage))
            {
                owner.isLeftSwipe = false;
                owner.targetPercentage = owner.swipePercentage + 1f;
                return Swiping;
            }

            return this;
        }
    }
}