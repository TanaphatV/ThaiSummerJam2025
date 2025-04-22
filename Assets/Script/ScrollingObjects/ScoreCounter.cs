using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public static ScoreCounter instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Score counter instance null");
            return _instance;
        }
    }
    private static ScoreCounter _instance;

    public int score { get; private set; } = 0;

    private void Awake()
    {
        _instance = this;
    }

    public void Restart()
    {
        score = 0;
    }

    public void AddScore(int score)
    {
        this.score += score;
    }

    public void RegisterScore()
    {
        int highScore = 0;
        if(PlayerPrefs.HasKey(PlayerPrefKey.HIGH_SCORE))
        {
            highScore = PlayerPrefs.GetInt(PlayerPrefKey.HIGH_SCORE);
        }
        if(score > highScore)
            PlayerPrefs.SetInt("HighScore", score);
    }
}
