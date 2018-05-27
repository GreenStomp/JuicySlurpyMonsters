using UnityEngine;
public class PlatManStart : MonoBehaviour
{
    public PlatformManager Manager;
    public Level Level;
    // Use this for initialization
    void Start()
    {
        Manager.ChangeLevel(Level);
        this.enabled = false;
    }
}