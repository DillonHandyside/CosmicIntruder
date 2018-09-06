using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public long score;
    public long hiScore;
    public int lives;

	// Use this for initialization
	void Awake ()
    {
        ResetScore();
        ResetLives();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (lives <= 0) { }
            //gameover;

        if (score > hiScore)
            hiScore = score;

        DebugKeys();
	}

    /// <summary>
    /// Decrements the player's lives
    /// </summary>
    void LoseLife()
    {
        lives--;

        if (lives < 0)
            lives = 0;
    }

    /// <summary>
    /// add the specified value to the player's score
    /// </summary>
    /// <param name="value">the amount of points to add</param>
    void AddScore(long value)
    {
        score += value;
    }

    /// <summary>
    /// sets the player's score to zero
    /// </summary>
    void ResetScore()
    {
        score = 0;
    }

    /// <summary>
    /// sets the player's current lives to three
    /// </summary>
    void ResetLives()
    {
        lives = 3;
    }

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
