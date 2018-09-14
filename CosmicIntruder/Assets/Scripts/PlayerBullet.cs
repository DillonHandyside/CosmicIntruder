using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public Player m_creator; //Reference to the script that fired this bullet
    public Vector2 m_speed; //Projectile peed

    // Update is called once per frame
    private void FixedUpdate()
    {
        //Force velocity to projectile speed every fixed update
        GetComponent<Rigidbody2D>().velocity = m_speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Any collision with the following tags will recycle the bullet to its creator
        if (collision.CompareTag("Border"))
        {
            ScoreManager.AddMissedShot(); // total missed shots += 1
            m_creator.DestroyBullet(gameObject);
        }
        else if (collision.CompareTag("Enemy"))
        {
            m_creator.DestroyBullet(gameObject);
        }
        else if (collision.CompareTag("Barrier"))
        {
            m_creator.DestroyBullet(gameObject);
        }
    }
}
