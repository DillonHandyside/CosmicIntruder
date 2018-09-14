using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

struct HiScore
{
    public string name;
    public int hiScore;
}

public class ScoreManager : MonoBehaviour
{
    // main display information
    static int hiScore; // the absolute highest score
    static int score; // player's current score
    static int lives; // player's current lives

    // kill counts
    static int enemyKills; // number of enemies destroyed
    static int mothershipKills; // number of motherships destroyed

    // accuracy
    static int totalShotsMissed; // number of shots that hit the boundaries
    static int totalShotsFired; // number of shots over player lifetime

	// Use this for initialization
	void Awake ()
    {
        ResetAll(); // set all values to default upon scene load
        hiScore = FileIO.Load(); // attempt to load hiScore
	}
	
	// Update is called once per frame
	void Update ()
    {
        DebugKeys();
    }

    /// <summary>
    /// helper function which calls all reset functions in score manager
    /// </summary>
    private void ResetAll()
    {
        ResetScore();
        ResetLives();
        ResetEnemyKills();
        ResetMothershipKills();
        ResetAccuracy();
    }

    /// <summary>
    /// returns the game's hi-score
    /// </summary>
    /// <returns>hi-score -  the highest score achieved before</returns>
    static public int GetHiScore()
    {
        return hiScore;
    }

    /// <summary>
    /// add the specified value to the player's score
    /// </summary>
    /// <param name="value">the amount of points to add</param>
    static public void AddScore(int value)
    {
        score += value;

        if (score > hiScore)
        {
            hiScore = score;
        }
    }

    /// <summary>
    /// sets the player's score to zero
    /// </summary>
    private void ResetScore()
    {
        score = 0;
    }

    /// <summary>
    /// return the player's current score
    /// </summary>
    /// <returns>score - player's current score</returns>
    static public int GetScore()
    {
        return score;
    }

    /// <summary>
    /// Decrements the player's lives and handles game over transition
    /// </summary>
    static public void LoseLife()
    {
        lives--;

        if (lives < 0)
        {
            SceneManager.LoadScene("gameOver");
            lives = 0;
        }
    }

    /// <summary>
    /// sets the player's current lives to three
    /// </summary>
    private void ResetLives()
    {
        lives = 3;
    }

    /// <summary>
    /// returns the number of remaining player lives
    /// </summary>
    /// <returns>lives - player's current lives</returns>
    static public int GetLives()
    {
        return lives;
    }

    /// <summary>
    /// 
    /// </summary>
    static public void InstantDeath()
    {
        lives = 0;
        SceneManager.LoadScene("gameOver");
    }

    /// <summary>
    /// increments the number of enemies killed by one
    /// </summary>
    static public void AddEnemyKill()
    {
        enemyKills++;
    }

    /// <summary>
    /// sets the tally of enemies destroyed to zero
    /// </summary>
    private void ResetEnemyKills()
    {
        enemyKills = 0;
    }

    /// <summary>
    /// returns the number of enemies destroyed
    /// </summary>
    /// <returns>enemyKills - number of enemies destroyed this game</returns>
    static public int GetEnemyKills()
    {
        return enemyKills;
    }

    /// <summary>
    /// increments the number of enemies killed by one
    /// </summary>
    static public void AddMothershipKill()
    {
        mothershipKills++;
    }

    /// <summary>
    /// sets the tally of motherships destroyed to zero
    /// </summary>
    private void ResetMothershipKills()
    {
        mothershipKills = 0;
    }

    /// <summary>
    /// returns the number of motherships destroyed
    /// </summary>
    /// <returns>mothershipKills - number of motherships destroyed this game</returns>
    static public int GetMothershipKills()
    {
        return mothershipKills;
    }

    /// <summary>
    /// increments the number of missed shots by one
    /// </summary>
    static public void AddMissedShot()
    {
        totalShotsMissed++;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    static public int GetMissedShots()
    {
        return totalShotsMissed;
    }

    /// <summary>
    /// increments the total number of fired shots by one
    /// </summary>
    static public void AddFiredShot()
    {
        totalShotsFired++;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    static public int GetFiredShots()
    {
        return totalShotsFired;
    }

    /// <summary>
    /// sets the number of shots fired and missed to zero
    /// </summary>
    private void ResetAccuracy()
    {
        totalShotsMissed = 0;
        totalShotsFired = 0;
    }

    /// <summary>
    /// function used to test and debug score manager
    /// </summary>
    void DebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            AddScore(500);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            LoseLife();
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            ResetScore();
        }
    }
}
