using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public EnemyManager m_enemyManager;
	//public GameObject m_bulletPrefab;

	public int m_nScoreValue;
	public float m_fDefaultShootTimer;
	private float m_fCurrentShootTimer;

	public float m_fBulletSpeed;

	private float m_fTimer;
	private bool m_bIsAlive;



	// Use this for initialization
	void Awake()
	{
		//Set alive to true and timer to zero. 
		m_bIsAlive = true;
		m_fTimer = 0.0f;
		m_fCurrentShootTimer = m_fDefaultShootTimer * Random.Range(0.5f, 2.0f);
	}
	
	// Update is called once per frame
	void Update()
    {
		m_fTimer += Time.deltaTime;

		if(m_fTimer >= m_fCurrentShootTimer)
		{
			m_enemyManager.Shoot(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), new Vector2(0, -1 * m_fBulletSpeed));
			m_fTimer = 0.0f;
			m_fCurrentShootTimer = Random.Range(0.5f, 2.0f) * m_fDefaultShootTimer;
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