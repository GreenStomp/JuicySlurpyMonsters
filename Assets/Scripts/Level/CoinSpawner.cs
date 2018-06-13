using UnityEngine;
using SOPRO;
using System.Collections.Generic;
public class CoinSpawner : MonoBehaviour
{
    public CoinManager CoinManager;
    public PlatformManager PlatManager;
    public SOBasicEvPlatform PlatformCreated;

    public ReferenceUint CoinsToSpawnPerPlatform;

    public Transform CoinParent;

    private Step step = new Step();
    private Step.Data data = new Step.Data();

    private Vector3[] spawnPoints;

    private void OnEnable()
    {
        step.Manager = PlatManager;
        spawnPoints = new Vector3[(int)CoinsToSpawnPerPlatform.Value];
        PlatformCreated.Event += SpawnCoinsOnPlat;
    }
    private void OnDisable()
    {
        PlatformCreated.Event -= SpawnCoinsOnPlat;
    }
    public void SpawnCoinsOnPlat(Platform plat)
    {
        step.Reset(data, plat, true, false);

        Transform platT = plat.transform;
        int length = spawnPoints.Length;
        float stepPercSize = 1.000001f / length;
        Lane lane = plat.Lanes[data.CurrentLane];
        int maxCount;
        for (maxCount = 0; maxCount < length; maxCount++)
        {
            if (step.CalculateNextStep(stepPercSize * lane.LocalCurve.Length, data))
                break;
            spawnPoints[maxCount] = platT.TransformPoint(data.LocalPosition);
        }

        int coinSpawned;
        CoinManager.SpawnCoins(spawnPoints, maxCount, out coinSpawned, CoinParent);
    }
}