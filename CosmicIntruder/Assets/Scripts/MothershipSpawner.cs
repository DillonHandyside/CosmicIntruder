using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothershipSpawner : MonoBehaviour
{
    //What is our mothership?
    public GameObject m_mothershipPrefab;
    //Spawn rate and randomness threshold for mothership spawning
    public float m_rate = 10.0f;
    public float m_rateVariance = 2.0f;
    //How far offscreen should the mothership spawn (and how high)
    public float m_spawnHeight = 10.0f;
    public float m_spawnX = 30.0f;
    //Travel speed of the mothership
    public float m_MothershipSpeed = 5.0f;
    //How much is a mothership worth
    public int m_MothershipScore = 500;
    //Countdown till next mothership
    private float m_timer;

    //Mothership object to reuse
    private GameObject m_mothership;

    private void Start()
    {
        //Start with maximum delay
        m_timer = m_rate + m_rateVariance;
        //Start mothership deactivated
        m_mothership = Instantiate<GameObject>(m_mothershipPrefab);
        m_mothership.SetActive(false);
    }

    // Update is called once per frame
    void Update ()
    {
        //Only decrease timer if the mothership is dead
        if (m_mothership.activeSelf == false)
        {
            m_timer -= Time.deltaTime;
        }
        //Spawn a mothership
        if (m_timer < 0.0f)
        {
            //Set the next countdown for another mothership
            m_timer = m_rate + ((Random.value * 2.0f - 1.0f) * m_rateVariance);
            //Activate the mothership
            m_mothership.SetActive(true);
            //Setup mothership values
            Mothership motherScript = m_mothership.GetComponent<Mothership>();
            //Copy values over to the mothership
            motherScript.m_speed = -m_MothershipSpeed;
            motherScript.m_score = m_MothershipScore;
            motherScript.m_suicideDistance = m_spawnX * -1.0f;
            motherScript.m_movingRight = false;
            //See if we are going to move left or right
            float trueX = m_spawnX;
            if (Random.value > 0.5f)
            {
                //Invert values if we are traveling in the other direction
                trueX *= -1.0f;
                motherScript.m_speed *= -1.0f;
                motherScript.m_suicideDistance *= -1.0f;
                motherScript.m_movingRight = true;
            }
            //Set the motherships position
            m_mothership.transform.position = new Vector3(trueX, m_spawnHeight);
        }
    }
}
