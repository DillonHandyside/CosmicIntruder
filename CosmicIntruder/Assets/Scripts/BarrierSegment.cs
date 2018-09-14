using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierSegment : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Get touched by an enemy or any bullet and just deactivate
        if (collision.CompareTag("Bullet"))
        {
            gameObject.SetActive(false);
        }
        else if (collision.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
        }
		else if (collision.CompareTag("EnemyBullet"))
		{
			gameObject.SetActive(false);
		}
    }

}
