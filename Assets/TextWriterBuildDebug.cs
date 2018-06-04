using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOPRO.Variables;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]
public class TextWriterBuildDebug : MonoBehaviour
{
    public ReferenceFloat DistanceWalked;
    public ReferenceUint PlatformsPassed;
    public ReferenceUint SpecialPlatformsPassed;
    public ReferenceFloat DEBUG_Count;
    public Transform Player;
    Text text;
    Step.Data data;
    private void Start()
    {
        text = GetComponent<Text>();
        data = FindObjectOfType<PlayerController>().StepData;
    }
    // Update is called once per frame
    void Update()
    {
        text.text = "Dist Walked " + (int)(DistanceWalked == null ? 0 : DistanceWalked.Value) + " , Plat passed " + (PlatformsPassed == null ? 0 : PlatformsPassed.Value) + " , curr plat " + data.Plat + " , Player pos " + Player.position + " , Debug c " + (int)(DEBUG_Count.Value);

    }
}
