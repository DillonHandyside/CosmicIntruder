using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public Player m_creator;
    public Vector2 m_speed;

    // Update is called once per frame
    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = m_speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Border"))
        {
            m_creator.DestroyBullet(gameObject);
        }
        else if (collision.CompareTag("Enemy"))
        {
            //destroy enemy
            m_creator.DestroyBullet(gameObject);
        }
        else if (collision.CompareTag("Barrier"))
        {
            m_creator.DestroyBullet(gameObject);
        }
    }
}
