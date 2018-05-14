using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_01_Audio_Manager : MonoBehaviour
{
    private AudioSource audioSource;
    private int index;

    private bool isFirstCycle;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        index = 0;
        //when he call this callback he is first cicle
        isFirstCycle = true;
    }
    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            if (isFirstCycle)
            {
                Ultimate_Audio_Management.Instance.PlaySound(0);
                isFirstCycle = false;
            }
            else
            {

                if (index <= 3)
                    index++;
                else
                    index = 1;
            }
            Ultimate_Audio_Management.Instance.PlaySound(index);
        }
    }

}
