using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreSystem", menuName = "ScoreSystem")]
public class ScoreSystem : ScriptableObject
{
    private int score;
    public int Score { get { return score; } set { score = value; } }
    private int bestScore;
    public int BestScore { get { return bestScore; } }
   
    public void ReleaseScore()
    {
        score = 0;
    }
    public void UpdateScore()
    {
        score++;
    }
    public void UpdateScore(int amount)
    {
        score += amount;
    }
    public void CheckNewBestScore()
    {
        int lastScore = bestScore;
        bestScore = score > lastScore ? score : lastScore;
    }
}
