using UnityEngine;
public sealed class GameManager : MonoBehaviour
{
    public const string MonsterLayerName = "Monster";
    public const string HumanLayerName = "Human";
    public const string ObstacleDestroyerLayerName = "ObstacleDestroyer";
    public const string ObstacleLayerName = "Obstacle"; 
    public static int MonsterLayer { get; private set; }
    public static int HumanLayer { get; private set; }
    public static int ObstacleLayer { get; private set; }
    public static int ObstacleDestroyerLayer { get; private set; }

    uint prevPlatSurpassed;
    void Awake()
    {
        MonsterLayer = LayerMask.NameToLayer(MonsterLayerName);
        HumanLayer = LayerMask.NameToLayer(HumanLayerName);
        ObstacleLayer = LayerMask.NameToLayer(ObstacleLayerName);
        ObstacleDestroyerLayer = LayerMask.NameToLayer(ObstacleDestroyerLayerName);
    }
    void Start()
    {
        PlatformManager.Instance.SetNextSpecialPlatform<GradualSpeedUp>();
        prevPlatSurpassed = PlatformManager.Instance.PlatformsSurpassed;
    }

    void Update()
    {
        if (PlatformManager.Instance.PlatformsSurpassed == prevPlatSurpassed)
            return;
        prevPlatSurpassed = PlatformManager.Instance.PlatformsSurpassed;
        if (PlatformManager.Instance.PlatformsSurpassed % 7 == 0)
        {
            PlatformManager.Instance.SetNextSpecialPlatform<GradualSpeedUp>();
        }
        if (PlatformManager.Instance.PlatformsSurpassed % 11 == 0)
        {
            PlatformManager.Instance.SetNextSpecialPlatform<GradualSpeedDown>();
        }
        if (PlatformManager.Instance.PlatformsSurpassed % 22 == 0)
        {
            PlatformManager.Instance.SetNextSpecialPlatform<RevertControls>();
        }
        if (PlatformManager.Instance.PlatformsSurpassed % 50 == 0)
        {
            PlatformManager.Instance.SetNextSpecialPlatform<BulletTime>();
        }
        if (PlatformManager.Instance.PlatformsSurpassed % 45 == 0)
        {
            if (PlatformManager.Instance.PlatformsSurpassed % 2 == 0)
                PlatformManager.Instance.SetNextSpecialPlatform<SpeedUp>();
            else
                PlatformManager.Instance.SetNextSpecialPlatform<SpeedDown>();
        }
    }
}
