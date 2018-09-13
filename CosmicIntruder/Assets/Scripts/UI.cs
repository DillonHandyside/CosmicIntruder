using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text score; // the UI text element which shows score value
    public Text hiScore; // the UI text element which shows hiScore value
    public Image[] lives; // the (3) UI images which represents each life

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        // update the on-screen score and hiScore values
        score.text = ScoreManager.GetScore().ToString();
        hiScore.text = ScoreManager.GetHiScore().ToString();

        // update the on-screen lives
        int i = 0;
        foreach (Image life in lives)
        {
            if (i < ScoreManager.GetLives())
                life.enabled = true; // enable life image if life exists
            else
                life.enabled = false; // disable life image if non-existant
            
            i++;
        }
    }
}
