﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
	//Variables to set how many rows of enemies and how many columns of enemies there should be
	public int m_nEnemyColumns;
	public int m_nEnemyRows;

	//Variables to set the spacing between each enemy
	public Vector2 m_v2EnemySpacing;

	//Prefab for enemies
	public GameObject m_enemyPrefab;
	
	//Reference to the enemies
	private GameObject[] m_enemies;

	//How many enemies there are
	private int m_nMaxEnemies;
	private int m_nEnemyCount;

	//Variables for timing how long it takes between movements of enemies in seconds
	public float m_fInitialMovementTimer;
	public float m_fMinimumMovementTimer;

	//Private floats for time related things, calculated automatically
	private float m_fCurrentMovementTimer;
	private float m_fTimer;

	//How far to move each enemy on a movement step
	public float m_fMovementStep;
	public float m_fDownwardsStep;

	//Boolean to check if the enemies should be currently moving right. 
	private bool m_bMovingRight;

	//Boolean to check whether the enemies have moved down this movement cycle
	private bool m_bHitRightTrigger = false;
	private bool m_bHitLeftTrigger = false;

    //Bullet pool for enemies
    public GameObject m_bulletPrefab;
    public int m_iPoolAmount;
    private Stack<GameObject> m_readyBullets = new Stack<GameObject>();

    // access to the main audio source and move/death sound effects
    public AudioSource audioSource;
    public AudioClip moveSFX;
    public AudioClip deathSFX;

	// Use this for initialization
	void Awake()
	{
        //Initialise stack of bullets
        for (int i = 0; i < m_iPoolAmount; ++i)
        {
            GameObject o = Instantiate<GameObject>(m_bulletPrefab);
			o.GetComponent<EnemyBullet>().m_creator = this;
            m_readyBullets.Push(o);
            o.SetActive(false);
        }
		//Initialise array
		m_enemies = new GameObject[m_nEnemyColumns * m_nEnemyRows];

		//Set enemies and max enemies to how many enemies are set in the editor
		m_nMaxEnemies = m_nEnemyRows * m_nEnemyColumns;
		m_nEnemyCount = m_nMaxEnemies;

		//Set how long between movements to inital timer
		m_fCurrentMovementTimer = m_fInitialMovementTimer;

		//Set timer to zero
		m_fTimer = 0.0f;

		CreateEnemies();
	}
	
	// Update is called once per frame
	void Update()
	{
		//Update timer
		m_fTimer += Time.deltaTime;

		//If timer reaches point of time between movements, move all enemies
		if(m_fTimer >= m_fCurrentMovementTimer)
		{
			MoveEnemies();
			m_fTimer = 0.0f;
		}
	}

	public void CreateEnemies()
	{
		//nested for loop to created enemy dimensionality
		for(int x = 0; x < m_nEnemyColumns; x++)
		{
			for(int y = 0; y < m_nEnemyRows; y++)
			{
				m_enemies[x + (y * m_nEnemyColumns)] = Instantiate<GameObject>(m_enemyPrefab);
				m_enemies[x + (y * m_nEnemyColumns)].GetComponent<Enemy>().m_enemyManager = this;
				Vector3 pos = gameObject.transform.position;
				pos.x += (m_v2EnemySpacing.x * x + 1);
				pos.y -= (m_v2EnemySpacing.y * y + 1);
				m_enemies[x + (y * m_nEnemyColumns)].transform.position = pos;
			}
		}
	}

	public void EnemyKilled(GameObject enemy)
	{
		//Reduce enemy count
		m_nEnemyCount -= 1;

		//Decrease time it takes between movement steps
		m_fCurrentMovementTimer = m_fMinimumMovementTimer + ((m_fInitialMovementTimer - m_fMinimumMovementTimer) * ((float)m_nEnemyCount / m_nMaxEnemies));

        audioSource.PlayOneShot(deathSFX);

		//call score manager to add enemy kill and add score
		ScoreManager.AddScore(enemy.GetComponent<Enemy>().m_nScoreValue);
		ScoreManager.AddEnemyKill();

		//if no more enemies, end game
		if(m_nEnemyCount <= 0)
		{
			SceneManager.LoadScene("gameOver");
		}
	}

	private void MoveEnemies()
	{
		//go through all enemies
		foreach (GameObject enemy in m_enemies)
		{
			//if enemy is alive, begin moving
			if(enemy.GetComponent<Enemy>().GetAlive())
			{
				Vector3 pos = enemy.transform.position;

				//check what direction enemies are moving
				if(m_bMovingRight)
				{
					pos.x += m_fMovementStep;
					enemy.transform.position = pos;
				}

				else
				{
					pos.x -= m_fMovementStep;
					enemy.transform.position = pos;
				}
			}
		}

        audioSource.PlayOneShot(moveSFX);
	}

	private void MoveEnemiesDown()
	{
		//go through all enemies and move them down
		foreach (GameObject enemy in m_enemies)
		{
			Vector3 pos = enemy.transform.position;
			pos.y -= m_fDownwardsStep;
			enemy.transform.position = pos;
		}
	}

	public void SwitchDirections(bool leftTriggerHit)
	{
		//set the moving right variable to whatever was passed in to the function from enemy
		m_bMovingRight = leftTriggerHit;
		//if the left trigger has been hit for the first time since we hit the right trigger, move enemies down
		if (leftTriggerHit && !m_bHitLeftTrigger)
		{
			m_bHitLeftTrigger = true;
			m_bHitRightTrigger = false;
			MoveEnemiesDown();
		}

		//if the right trigger has been hit for the first time since we hit the left trigger, move enemies down
		else if (!leftTriggerHit && !m_bHitRightTrigger)
		{
			m_bHitRightTrigger = true;
			m_bHitLeftTrigger = false;
			MoveEnemiesDown();
		}
	}

    //Shooting
    public void Shoot(Vector2 _position, Vector2 _fire_vector)
    {
		//check if there are any ready enemy bullets, if there are not, exit the function
		if (m_readyBullets.Count == 0)
			return;

		//get bullet from top of object pool, set position and speed
        GameObject top = m_readyBullets.Pop();
        top.GetComponent<EnemyBullet>().m_speed = _fire_vector;
        top.transform.position = new Vector2(_position.x, _position.y);

		//set object active
        top.SetActive(true);
    }

    //Bullet destroy event
    public void DestroyBullet(GameObject _bullet)
    {
		//disable bullet and return to ready array
        _bullet.SetActive(false);
        m_readyBullets.Push(_bullet);
    }
}
