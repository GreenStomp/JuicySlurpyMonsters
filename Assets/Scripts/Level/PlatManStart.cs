using UnityEngine;
using System.Collections;
public class PlatManStart : MonoBehaviour
{
    public PlatformManager Manager;
    public Level Level;
    WaitForSeconds wait = new WaitForSeconds(3);
    // Use this for initialization
    void Start()
    {
        Manager.ChangeLevel(Level);
        StartCoroutine(UpdManager());
        this.enabled = false;
    }
    IEnumerator UpdManager()
    {
        while (true)
        {
            yield return wait;
            Manager.UpdateActivePlatforms();

        }
    }
}