using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CustomTimerTest : MonoBehaviour
{
    public int numTests = 100000;
    private void OnGUI()
    {
        GUI.skin.button.fontSize = 50;
        if (GUI.Button(new Rect(0, 0, 400, 100), "Test"))
        {
            using (new CustomTimer("Empty Cycle Test", numTests))
            {
                for (int i = 0; i < numTests; i++)
                {
                    TestFunction();

                }
            }
        }
    }
    void TestFunction()
    {

    }
}
