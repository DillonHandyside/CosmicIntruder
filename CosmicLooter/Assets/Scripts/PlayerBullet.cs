using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public Player m_creator;

    public int m_power;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Border"))
        {
            m_creator.DestroyBullet(gameObject);
        }
        if (collision.tag.Equals("Enemy"))
        {
            //Deal damage90
            m_creator.DestroyBullet(gameObject);
        }
    }
}
