using UnityEngine;
using SOPRO;
public class CoinSpawner : MonoBehaviour
{
    public CoinManager CoinManager;

    public SOBasicEvPlatform PlatformCreated;

    public ReferenceUint CoinsToSpawnPerPlatform;

    public Transform CoinParent;

    private Vector3[] spawnPoints;

    private void OnEnable()
    {
        spawnPoints = new Vector3[(int)CoinsToSpawnPerPlatform.Value];
        PlatformCreated.Event += SpawnCoinsOnPlat;
    }
    private void OnDisable()
    {
        PlatformCreated.Event -= SpawnCoinsOnPlat;
    }
    public void SpawnCoinsOnPlat(Platform plat)
    {
        Transform platT = plat.transform;
        int length = spawnPoints.Length;
        float stepPercSize = 1.000001f / (length + 1);
        Lane lane = plat.Lanes[Random.Range(0, plat.Lanes.Length)];
        int maxCount;
        int validPoints = lane.LocalCurve.ValidPoints;
        for (maxCount = 0; maxCount < length; maxCount++)
        {
            Vector3 pos = validPoints == 2 ? lane.LocalCurve.CalculateBezierFirst(stepPercSize * maxCount) : (validPoints == 3 ? lane.LocalCurve.CalculateQuadraticBezier(stepPercSize * maxCount) : lane.LocalCurve.CalculateCubicBezier(stepPercSize * maxCount));
            spawnPoints[maxCount] = platT.TransformPoint(pos);
        }

        int coinSpawned;
        CoinManager.SpawnCoins(spawnPoints, maxCount, out coinSpawned, CoinParent);
    }
}