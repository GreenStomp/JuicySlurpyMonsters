/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIDPlatformCustom : MonoBehaviour
{

    public uint[] IdsToSpawn;
    // Use this for initialization
    void Start()
    {
        PlatformManager.Instance.Restart(0, IdsToSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlatformManager.Instance.ProgrammedPlatformsCount < 2)
        {
            for (int i = 0; i < IdsToSpawn.Length; i++)
            {
                PlatformManager.Instance.SetNextPlatform(IdsToSpawn[i]);
            }
        }
    }
}
*/