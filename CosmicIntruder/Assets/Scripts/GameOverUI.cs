using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    // multipliers
    public float accuracyMultiplier = 0.1f; // the scale factor of accuracy multiplier
    public float mothershipKillMultiplier = 0.5f; // the scale factor of mothership multiplier
    public float livesRemainingMultiplier = 0.1f; // the scale factor of lives remaining multiplier
    private float multiplierValue = 1.0f; // the final multiplier, should not fall below 1.0f

    // UI text fields to modify
    public Text score;
    public Text accuracy;
    public Text mothershipsDestroyed;
    public Text livesRemaining;
    public Text multiplier;
    public Text totalScore;

	// Use this for initialization
	void Awake ()
    {
        PrintAll();
        CheckHiScore(); // update hiScore if need be
    }
	
	// Update is called once per frame
	void Update ()
    {
    }

    /// <summary>
    /// Final hiScore check which calls save if the final score is greater 
    /// than the current hiScore
    /// </summary>
    void CheckHiScore()
    {
        int finalScore = (int)(ScoreManager.GetScore() * multiplierValue);

        if (finalScore > ScoreManager.GetHiScore())
            FileIO.Save(finalScore);
    }

    /// <summary>
    /// change the values of all relevant text fields in game over screen upon wakeup
    /// </summary>
    void PrintAll()
    {
        PrintScore();
        PrintMothershipsDestroyed();
        PrintAccuracy();
        PrintLivesRemaining();
        PrintMultiplier();
        PrintTotalScore();
    }

    /// <summary>
    /// changes the text value of UI element "score"
    /// </summary>
    void PrintScore()
    {
        // print
        score.text = ScoreManager.GetScore().ToString();
    }

    /// <summary>
    /// changes the text value of UI element "motherships destroyed"
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
    /// changes the text value of UI element "lives remaining"
    /// </summary>
    void PrintLivesRemaining()
    {
        int numberOfLivesRemaining = ScoreManager.GetLives();

        // print
        livesRemaining.text = numberOfLivesRemaining.ToString();

        // add kills to multiplier
        multiplierValue += numberOfLivesRemaining * livesRemainingMultiplier;
    }

    /// <summary>
    /// changes the text value of UI element "accuracy"
    /// </summary>
    void PrintAccuracy()
    {
        float acc = 0.0f;

        // prevent division by zero
        if (ScoreManager.GetFiredShots() > 0)
            acc = (float)(ScoreManager.GetFiredShots() - ScoreManager.GetMissedShots()) / (float)ScoreManager.GetFiredShots();

        // add accuracy to multiplier
        multiplierValue += Mathf.Exp(acc) - 1;

        // print in "0.00%" format
        accuracy.text = (acc * 100.0f).ToString("0") + "%";
    }

    /// <summary>
    /// changes the text value of UI element "multiplier"
    /// </summary>
    void PrintMultiplier()
    {
        // print in "0.00%" format
        multiplier.text = multiplierValue.ToString("0.00") + "x";
    }

    /// <summary>
    /// changes the text value of UI element "total score"
    /// </summary>
    void PrintTotalScore()
    {
        // total score = score * multiplier
        int totalScoreValue = (int)(ScoreManager.GetScore() * multiplierValue);

        // print
        totalScore.text = totalScoreValue.ToString();
    }
}
