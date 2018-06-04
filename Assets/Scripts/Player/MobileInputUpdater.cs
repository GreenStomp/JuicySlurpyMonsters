using UnityEngine;
public class MobileInputUpdater : MonoBehaviour
{
    public MobileInput Input;
    void Update()
    {
        this.Input.UpdateInput(Time.deltaTime);
    }
}