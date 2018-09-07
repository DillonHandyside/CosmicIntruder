using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    // multipliers
    public float accuracyMultiplier = 0.1f; // the scale factor of accuracy multiplier
    public float enemyKillMultiplier = 0.1f; // the scale factor of enemy multiplier
    public float mothershipKillMultiplier = 0.5f; // the scale factor of mothership multiplier
    private float multiplierValue = 1.0f; // the final multiplier, should not fall below 1.0f

    // UI text fields to modify
    public Text score;
    public Text accuracy;
    public Text enemiesDestroyed;
    public Text mothershipsDestroyed;
    public Text multiplier;
    public Text totalScore;

	// Use this for initialization
	void Awake ()
    {
        PrintAll();
    }
	
	// Update is called once per frame
	void Update ()
    {
    }

    /// <summary>
    /// 
    /// </summary>
    void PrintAll()
    {
        PrintScore();
        PrintEnemiesDestroyed();
        PrintMothershipsDestroyed();
        PrintAccuracy();
        PrintMultiplier();
        PrintTotalScore();
    }

    /// <summary>
    /// 
    /// </summary>
    void PrintScore()
    {
        // print
        score.text = ScoreManager.GetScore().ToString();
    }

    /// <summary>
    /// 
    /// </summary>
    void PrintEnemiesDestroyed()
    {
        int enemyKills = ScoreManager.GetEnemyKills();

        // print
        enemiesDestroyed.text = enemyKills.ToString();

        // add kills to multiplier
        multiplierValue += enemyKills * enemyKillMultiplier;
    }

    /// <summary>
    /// 
    /// </summary>
    void PrintMothershipsDestroyed()
    {
        int mothershipKills = ScoreManager.GetMothershipKills();
        
        // print
        mothershipsDestroyed.text = mothershipKills.ToString();

        // add kills to multiplier
        multiplierValue += mothershipKills * mothershipKillMultiplier;
    }

    /// <summary>
    /// 
    /// </summary>
    void PrintAccuracy()
    {
        float acc = 0.0f;

        // prevent division by zero
        if (ScoreManager.GetFiredShots() > 0)
            acc = (float)(ScoreManager.GetFiredShots() - ScoreManager.GetMissedShots()) / (float)ScoreManager.GetFiredShots();

        // add accuracy to multiplier
        multiplierValue += acc * accuracyMultiplier;

        // print in "0.00%" format
        accuracy.text = (acc * 100.0f).ToString("0.00") + "%";
    }

    /// <summary>
    /// 
    /// </summary>
    void PrintMultiplier()
    {
        // print in "0.00%" format
        multiplier.text = multiplierValue.ToString("0.00");
    }

    /// <summary>
    /// 
    /// </summary>
    void PrintTotalScore()
    {
        // total score = score * multiplier
        int totalScoreValue = (int)(ScoreManager.GetScore() * multiplierValue);

        // print
        totalScore.text = totalScoreValue.ToString();
    }
}
