using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mothership : MonoBehaviour
{
    public float m_speed;

    public float m_rotationRate;
    private float m_rotation;

    public int m_score = 0;

    public bool m_movingRight;
    public float m_suicideDistance;
	
	// Update is called once per frame
	void Update ()
    {
        m_rotation += m_rotationRate * Time.deltaTime;

        Quaternion quat = new Quaternion();
        quat.eulerAngles = new Vector3(0.0f, m_rotation, 0.0f);

        transform.rotation = quat;
        //Kill the mothership if it leaves the screen
        if (m_movingRight)
        {
            if (transform.position.x > m_suicideDistance)
            {
                Dead();
            }
        }
        else
        {
            if (transform.position.x < m_suicideDistance)
            {
                Dead();
            }
        }
    }

    private void Dead()
    {
        transform.position = new Vector3(-999.0f, -999.0f);
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(m_speed * Time.fixedDeltaTime, 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            ScoreManager.AddScore(m_score);
            ScoreManager.AddMothershipKill();

            Dead();
        }
    }
}
