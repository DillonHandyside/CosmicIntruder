using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierSegment : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            gameObject.SetActive(false);
        }
        else if (collision.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
        }
    }

}
