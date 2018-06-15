using UnityEngine;
using SOPRO;
public class PlayerController : MonoBehaviour
{
    public MobileInput Input;

    public PlatformNavigator Navigator;

    public AnimatorPropertyHolder Speed;
    public Animator Animator;

    private IState current;

    void Start()
    {
        Swiping swipe = new Swiping(this);
        Idle idle = new Idle(this);

        swipe.Idle = idle;
        idle.Swiping = swipe;

        current = idle;
    }
    void Update()
    {
        current = current.OnStateUpdate();
        SOPRO.AnimatorUtility.SetFloat(Animator, Speed, Navigator.MovementSpeed);
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
            Step.Data data = owner.Navigator.StepData;

            data.LaneLerpPercentage += Time.deltaTime * owner.Navigator.SwitchLaneLerpSpeed.Value;

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
            Step.Data data = owner.Navigator.StepData;
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