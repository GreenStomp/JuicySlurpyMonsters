using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOPRO;
public class PlatformNavigator : MonoBehaviour
{
    public ReferenceFloat MovementSpeed;
    public ReferenceFloat SwitchLaneLerpSpeed;

    public bool UseTranfrom = true;

    public bool ResetDataStructureOnEnable = false;

    public Step Step = new Step();

    public Step.Data StepData = new Step.Data();

    [SerializeField]
    private Transform myTransform;

    void OnEnable()
    {
        Step.Reset(StepData, Step.Manager.FirstPlatform, ResetDataStructureOnEnable, ResetDataStructureOnEnable);
    }
    void Start()
    {
        Step.Reset(StepData, Step.Manager.FirstPlatform, ResetDataStructureOnEnable, ResetDataStructureOnEnable);
    }

    public void Update()
    {
        if (UseTranfrom)
            Step.CalculateNextStep(myTransform, MovementSpeed * Time.deltaTime, StepData);
        else
            Step.CalculateNextStep(MovementSpeed * Time.deltaTime, StepData);

        Transform plat = StepData.Plat.transform;

        myTransform.LookAt(plat.TransformDirection(StepData.LocalForward) + myTransform.position, plat.TransformDirection(StepData.LocalUp));

        myTransform.position = plat.TransformPoint(StepData.LocalPosition);
    }
}
