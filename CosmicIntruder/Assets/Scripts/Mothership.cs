using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mothership : MonoBehaviour
{
    [HideInInspector]
    public float m_speed;
    [HideInInspector]
    public int m_score = 0;
    [HideInInspector]
    public bool m_movingRight;
    [HideInInspector]
    public float m_suicideDistance;
	
	// Update is called once per frame
	void Update ()
    {
        //Kill the mothership if it leaves the screen
        if (m_movingRight)
        {
            //Threshold reached
            if (transform.position.x > m_suicideDistance)
            {
                Dead();
            }
        }
        else
        {
            //Threshold reached
            if (transform.position.x < m_suicideDistance)
            {
                Dead();
            }
        }
    }

    private void Dead()
    {
        //Get deactivated and sent far away
        transform.position = new Vector3(-999.0f, -999.0f);
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        //Move around!
        transform.position += new Vector3(m_speed * Time.fixedDeltaTime, 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Die if we get hit by a bullet
        if (collision.CompareTag("Bullet"))
        {
            //Score and killcount for killing mothership
            ScoreManager.AddScore(m_score);
            ScoreManager.AddMothershipKill();

            Dead();
        }
    }
}
