using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGuiTest : MonoBehaviour
{

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 100), transform.GetComponent<Renderer>().material.shader.name);
    }
}
