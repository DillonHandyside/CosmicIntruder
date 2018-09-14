using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	//Enemy manager
	public EnemyManager m_enemyManager;

	//How many points the enemy is worth
	public int m_nScoreValue;

	//Default shoot speed, and how long until this enemy shoots next
	public float m_fDefaultShootTimer;
	private float m_fCurrentShootTimer;

	//how fast bullets move
	public float m_fBulletSpeed;

	//timer, incremented every frame by deltatime
	private float m_fTimer;

	//variable to check if enemy is alive
	private bool m_bIsAlive;



	// Use this for initialization
	void Awake()
	{
		//Set alive to true and timer to zero. 
		m_bIsAlive = true;
		m_fTimer = 0.0f;

		//randomises shoot timer
		m_fCurrentShootTimer = m_fDefaultShootTimer * Random.Range(0.5f, 2.0f);
	}
	
	// Update is called once per frame
	void Update()
    {
		//increment timer
		m_fTimer += Time.deltaTime;

		//if ready to shoot, shoot
		if(m_fTimer >= m_fCurrentShootTimer)
		{
			//calls enemy manager to shoot a bullet from the bullet pool
			m_enemyManager.Shoot(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), new Vector2(0, -1 * m_fBulletSpeed));

			//reset timer to zero
			m_fTimer = 0.0f;

			//set shoot timer to new random number
			m_fCurrentShootTimer = Random.Range(0.5f, 2.0f) * m_fDefaultShootTimer;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//check collisions
		//if left or right trigger, switch directions of all enemies
		if (collision.tag == "LeftTrigger")
		{
			m_enemyManager.GetComponent<EnemyManager>().SwitchDirections(true);
		}
		else if (collision.tag == "RightTrigger")
		{
			m_enemyManager.GetComponent<EnemyManager>().SwitchDirections(false);
		}

		//if bullet, enemy dies
		else if (collision.tag == "Bullet")
		{
			m_enemyManager.EnemyKilled(gameObject);
			m_bIsAlive = false;
			gameObject.SetActive(false);
		}

		//if bottom level border, player dies
        else if (collision.tag == "Border")
        {
            ScoreManager.InstantDeath();
        }
	}

	public bool GetAlive()
	{
		//check if enemy is alive
		return m_bIsAlive;
	}
}