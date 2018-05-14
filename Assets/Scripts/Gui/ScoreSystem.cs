using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem Instance;
    public Text ScoreText;

    public int Score { get { return score + (int)(p.TotalDistanceWalked * 0.05f) + (int)p.TotalPlatformsPassed + (int)p.SpecialPlatformsPassed; } }

    public string ScoreToString { get { return Score.ToString(); } }

    private int score;
    PlayerController p;
    private void Awake()
    {
        Instance = this;
        score = 0;
    }
    private void Start()
    {
        p = FindObjectOfType<PlayerController>();
    }
    public void LateUpdate()
    {
        ScoreText.text = Score.ToString();
    }

    public void UpdatingScore_Positive(int amount)
    {
        score += amount;
    }
    public void UpdatingScore_Negative(int amount)
    {
        score -= amount;
    }
}
