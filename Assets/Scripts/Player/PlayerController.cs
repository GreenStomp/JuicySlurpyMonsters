using UnityEngine;
using SOPRO.Variables;
public class PlayerController : MonoBehaviour
{
    public ReferenceFloat MovementSpeed;
    public ReferenceFloat SwitchLaneLerpSpeed;

    public Step Step = new Step();

    public Step.Data StepData = new Step.Data();

    public MobileInput Input;

    private IState current;

    private Transform myTransform;

    void Start()
    {
        myTransform = transform;

        Swiping swipe = new Swiping(this);
        Idle idle = new Idle(this);

        swipe.Idle = idle;
        idle.Swiping = swipe;

        current = idle;
    }
    void Update()
    {
        Step.CalculateNextStep(myTransform, Time.deltaTime * MovementSpeed.Value, StepData);

        Transform plat = StepData.Plat.transform;

        myTransform.LookAt(plat.TransformDirection(StepData.LocalForward) + myTransform.position, plat.TransformDirection(StepData.LocalUp));

        //current = current.OnStateUpdate();

        myTransform.position = plat.TransformPoint(StepData.LocalPosition);
    }
    private class Swiping : IState
    {
        private PlayerController owner;
        public IState Idle;
        public Swiping(PlayerController owner)
        {
            this.owner = owner;
        }
        public IState OnStateUpdate()
        {
            Step.Data data = owner.StepData;

            data.LaneLerpPercentage += Time.deltaTime * owner.SwitchLaneLerpSpeed.Value;

            if (data.LaneLerpPercentage > 1f)
            {
                data.CurrentLane = data.DestinationLane;
                data.IsSwitchingLanes = false;
                data.LaneLerpPercentage = 0f;
                return Idle;
            }

            return this;
        }
    }
    private class Idle : IState
    {
        private PlayerController owner;
        public IState Swiping;
        public Idle(PlayerController owner)
        {
            this.owner = owner;
        }
        public IState OnStateUpdate()
        {
            Step.Data data = owner.StepData;
            MobileInput input = this.owner.Input;

            if (input.SwipeLeft && data.CurrentLane > 0)
            {
                data.IsSwitchingLanes = true;
                data.DestinationLane = data.CurrentLane - 1;
                data.LaneLerpPercentage = 0f;
                return Swiping;
            }
            else if (input.SwipeRight && data.CurrentLane < data.Plat.Lanes.Length - 1)
            {
                data.IsSwitchingLanes = true;
                data.DestinationLane = data.CurrentLane + 1;
                data.LaneLerpPercentage = 0f;
                return Swiping;
            }

            return this;
        }
    }
}