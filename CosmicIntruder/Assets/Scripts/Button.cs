using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (tag)
        {
            case "PlayButton":
                SceneManager.LoadScene("game");
                break;
            case "MenuButton":
                SceneManager.LoadScene("mainMenu");
                break;
            case "QuitButton":
                Application.Quit();
                break;
        }
    }
}
