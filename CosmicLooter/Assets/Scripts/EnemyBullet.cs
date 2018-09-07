using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public EnemyManager m_creator;
    public Vector2 m_speed;

    public static float m_flipRate = 0.5f;
    private float m_timer = 0.0f;

    private void Update()
    {
        m_timer += Time.deltaTime;

        if (m_timer > m_flipRate)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1.0f;
            transform.localScale = scale;
        }
    }
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
        else if (collision.CompareTag("Player"))
        {
            m_creator.DestroyBullet(gameObject);
        }
        else if (collision.CompareTag("Barrier"))
        {
            m_creator.DestroyBullet(gameObject);
        }
    }
}
