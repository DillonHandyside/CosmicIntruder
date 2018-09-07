using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public EnemyManager m_enemyManager;
	//public GameObject m_bulletPrefab;

	public int m_nScoreValue;
	public float m_fShootTimer;

	private float m_fTimer;
	private bool m_bIsAlive;

	private Rigidbody2D rb;


	// Use this for initialization
	void Awake()
	{
		//Set alive to true and timer to zero. 
		m_bIsAlive = true;
		m_fTimer = 0.0f;

		//get rigidbody
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update()
    {
		m_fTimer += Time.deltaTime;

		if(m_fTimer >= m_fShootTimer)
		{
			//shoot code
			m_fTimer = 0.0f;
			//Instantiate<GameObject>(m_bulletPrefab);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "LeftTrigger")
		{
			m_enemyManager.GetComponent<EnemyManager>().SwitchDirections(true);
		}
		else if (collision.tag == "RightTrigger")
		{
			m_enemyManager.GetComponent<EnemyManager>().SwitchDirections(false);
		}
		else if (collision.tag == "Bullet")
		{
			m_enemyManager.EnemyKilled(gameObject);
			m_bIsAlive = false;
			gameObject.SetActive(false);
		}
	}

	public bool GetAlive()
	{
		return m_bIsAlive;
	}
}