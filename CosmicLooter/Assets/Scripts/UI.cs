using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public ScoreManager scoreManager;

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
        score.text = scoreManager.score.ToString();
        hiScore.text = scoreManager.hiScore.ToString();

        int i = 0;

        foreach (Image life in lives)
        {
            if (i < scoreManager.lives)
                life.enabled = true;
            else
                life.enabled = false;
            
            i++;
        }
    }
}
