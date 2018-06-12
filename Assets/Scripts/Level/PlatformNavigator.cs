using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOPRO;
public class PlatformNavigator : MonoBehaviour
{
    public ReferenceFloat MovementSpeed;
    public ReferenceFloat SwitchLaneLerpSpeed;

    public Step Step = new Step();

    public Step.Data StepData = new Step.Data();

    public bool ResetDataStructureOnEnable = false;

    [SerializeField]
    private Transform myTransform;
    // Use this for initialization
    void OnEnable()
    {
        Step.Reset(StepData, ResetDataStructureOnEnable, ResetDataStructureOnEnable);
    }

    // Update is called once per frame
    void Update()
    {
        Step.CalculateNextStep(myTransform, MovementSpeed * Time.deltaTime, StepData);

        Transform plat = StepData.Plat.transform;

        myTransform.LookAt(plat.TransformDirection(StepData.LocalForward) + myTransform.position, plat.TransformDirection(StepData.LocalUp));

        myTransform.position = plat.TransformPoint(StepData.LocalPosition);
    }
}
