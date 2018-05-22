using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public struct AudioCustom
{
    public AudioClip Clip;
    public string Name;
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

    private Dictionary<string, AudioClip> clipsDictionary;
    //performance increases if a preallocation exist
    private const int Preallocation = 15;

    private void Awake()
    {
        //sigleton initialization (always first!)
        Instance = this;
        //------------------------
        //require compnent item
        mainAudioPlayer = GetComponent<AudioSource>();
        clipsDictionary = new Dictionary<string, AudioClip>(Preallocation);
    }

    private void Start()
    {
        //add audioCustoms items to the dictionary
        for (int i = 0; i < AudioCustomsSounds.Count; i++)
        {
            clipsDictionary.Add(AudioCustomsSounds[i].Name, AudioCustomsSounds[i].Clip);
        }
    }

    public void PlaySound(string Name)
    {
        if (!clipsDictionary.ContainsKey(Name))
        {
            Debug.LogWarning("Key Value doesn't exist yet. Please intialize it first!");
        }
        //dictionary contains the key Name
        else
        {
            mainAudioPlayer.clip = clipsDictionary[Name];
            mainAudioPlayer.Play();

        }
    }
}
