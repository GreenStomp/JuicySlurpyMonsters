using UnityEngine;
[CreateAssetMenu(menuName = "Level/CoinSpawner")]
public class CoinSpawner : ScriptableObject
{
    public CoinManager CoinManager;
    public PlatformManager PlatManager;

    private Step step = new Step();
    private Step.Data data = new Step.Data();

    public void SpawnCoinsOnPlat(Platform plat)
    {

    }
}