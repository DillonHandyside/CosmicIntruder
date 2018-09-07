using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text score;
    public Text hiScore;
    public Image[] lives;

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        score.text = ScoreManager.GetScore().ToString();
        hiScore.text = ScoreManager.GetHiScore().ToString();

        int i = 0;

        foreach (Image life in lives)
        {
            if (i < ScoreManager.GetLives())
                life.enabled = true;
            else
                life.enabled = false;
            
            i++;
        }
    }
}
