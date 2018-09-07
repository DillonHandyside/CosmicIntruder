﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	// Use this for initialization
	void Awake()
	{
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

		ScoreManager.AddScore(enemy.GetComponent<Enemy>().m_nScoreValue);
		ScoreManager.AddEnemyKill();
	}

	private void MoveEnemies()
	{
		foreach (GameObject enemy in m_enemies)
		{
			if(enemy.GetComponent<Enemy>().GetAlive())
			{
				Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
				Vector3 pos = enemy.transform.position;

				if(m_bMovingRight)
				{
					pos.x += m_fMovementStep;
					rb.position = pos;
				}

				else
				{
					pos.x -= m_fMovementStep;
					rb.position = pos;
				}
			}
		}
	}

	private void MoveEnemiesDown(bool leftTriggerHit)
	{
		if(!m_bHitLeftTrigger || !m_bHitRightTrigger)
		{
			foreach(GameObject enemy in m_enemies)
			{
				Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
				Vector3 pos = enemy.transform.position;
				pos.y -= m_fDownwardsStep;
				rb.position = pos;
			}
		}
	}

	public void SwitchDirections(bool leftTriggerHit)
	{
		m_bMovingRight = leftTriggerHit;
		if (leftTriggerHit)
		{
			m_bHitRightTrigger = false;
			MoveEnemiesDown(leftTriggerHit);
		}
		else
		{
			m_bHitLeftTrigger = false;
			MoveEnemiesDown(leftTriggerHit);
		}
	}
}
