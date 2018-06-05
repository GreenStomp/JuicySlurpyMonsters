using UnityEngine;
using System.Collections;
public class PlatManStart : MonoBehaviour
{
    public PlatformManager Manager;
    public Level Level;

    private WaitForSeconds wsr = new WaitForSeconds(3f);
    // Use this for initialization
    void Start()
    {
        Manager.ChangeLevel(Level);
        StartCoroutine(UpdatePlatMan());
        this.enabled = false;
    }
    IEnumerator UpdatePlatMan()
    {
        while (true)
        {
            Manager.UpdateActivePlatforms();
            yield return wsr;
        }
    }
}