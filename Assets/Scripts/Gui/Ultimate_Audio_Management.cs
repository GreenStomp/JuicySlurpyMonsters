using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public struct AudioCustom
{
    public AudioClip Clip;
    public int Id;
}

[RequireComponent(typeof(AudioSource))]
public class Ultimate_Audio_Management : MonoBehaviour
{
    //Singleton
    public static Ultimate_Audio_Management Instance;
    //--------------------------

    private AudioSource mainAudioPlayer;

    [Header("SFX-Sounds")]
    public List<AudioCustom> AudioCustomsSounds;

    private Dictionary<int, AudioClip> clipsDictionary;
    //performance increases if a preallocation exist
    private const int Preallocation = 15;

    private void Awake()
    {
        //sigleton initialization (always first!)
        Instance = this;
        //------------------------
        //require compnent item
        mainAudioPlayer = GetComponent<AudioSource>();
        clipsDictionary = new Dictionary<int, AudioClip>(Preallocation);
    }

    private void Start()
    {
        //add audioCustoms items to the dictionary
        for (int i = 0; i < AudioCustomsSounds.Count; i++)
        {
            clipsDictionary.Add(AudioCustomsSounds[i].Id, AudioCustomsSounds[i].Clip);
        }
        //always the index 0 of the list is setted to be the first in the dictionary
        mainAudioPlayer.clip = AudioCustomsSounds[0].Clip;
    }

    public void PlaySound(int Id)
    {
        if (!clipsDictionary.ContainsKey(Id))
        {
            Debug.LogWarning("Key Value doesn't exist yet. Please intialize it first!");
        }
        //dictionary contains the key Name
        else
        {
            mainAudioPlayer.clip = clipsDictionary[Id];
            mainAudioPlayer.Play();

        }
    }
}
