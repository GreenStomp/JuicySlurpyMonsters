using UnityEngine;
public class CoinSpawnerWaypoint : MonoBehaviour
{
    public bool IsOverriding { get { return overrideCoinsThisSegment; } }
    public int CoinsThisSegment { get { return coinsThisSegment; } }
    [SerializeField]
    private bool overrideCoinsThisSegment;
    [SerializeField]
    [Range(1, 500)]
    private int coinsThisSegment;
}