using UnityEngine;
public class DEBUGSpecialPlatformHandler : MonoBehaviour
{
    public void RequestSpeedUp()
    {
        PlatformManager.Instance.SetNextSpecialPlatform<SpeedUp>();
    }
    public void RequestSpeedDown()
    {
        PlatformManager.Instance.SetNextSpecialPlatform<SpeedDown>();
    }
    public void RequestGradualSpeedUp()
    {
        PlatformManager.Instance.SetNextSpecialPlatform<GradualSpeedUp>();
    }
    public void RequestGradualSpeedDown()
    {
        PlatformManager.Instance.SetNextSpecialPlatform<GradualSpeedDown>();
    }
    public void RequestRevertControls()
    {
        PlatformManager.Instance.SetNextSpecialPlatform<RevertControls>();
    }
    public void RequestBulletTime()
    {
        PlatformManager.Instance.SetNextSpecialPlatform<BulletTime>();
    }
}