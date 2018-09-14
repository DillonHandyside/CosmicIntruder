using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Player movement speed
    public float m_speed = 10.0f;
    //Delay until the next bullet can be fired
    public float m_fireDelay = 1.0f;
    //Timer for shooting
    private float m_timer = 0.0f;
    //Vertical projectile travel speed 
    public float m_bulletSpeed = 10.0f;

    //Offset from the player position to spawn the bullet
    public Vector2 m_bulletOffset;

    //Players rigidbody
    private Rigidbody2D rb;

    //Player internal object pool
    public GameObject m_bullet;
    public int m_poolAmount;

    //Bullet Pool
    private Stack<GameObject> m_poolReady = new Stack<GameObject>();

    //Critical Health flash
    public Color m_color;
    public float m_flashDuration;
    public float m_flashDelay;
    //Private variables for flashing player
    private bool m_criticalState = false;
    private Color m_originalColor;
    private float m_critTimer = 0.0f;
    private bool m_flash = false;

	// Use this for initialization
	void Start ()
    {
        //Get the rigidbody for use in update later
        rb = GetComponent<Rigidbody2D>();
        //Generate the bullet pool, this wont change live if the pool amount is changed
        //m_pool = new GameObject[m_poolAmount];
        for (int i = 0; i < m_poolAmount; ++i)
        {
            GameObject obj = Instantiate<GameObject>(m_bullet);
            obj.GetComponent<PlayerBullet>().m_creator = this; //Tell all bullets ahead of time who's gonna shoot them
            obj.SetActive(false);
            m_poolReady.Push(obj);
        }
        //Get the original color of the players material
        m_originalColor = GetComponent<Renderer>().material.color;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Increase shot timer
        m_timer += Time.deltaTime;

        //Horizontal Movement
        RuntimePlatform currentPlatform = Application.platform;
        Vector2 vel = rb.velocity;

        // handle movement differently depending on platform
        switch (currentPlatform)
        {
            case RuntimePlatform.Android:
                if (Input.GetJoystickNames().Length == 0)
                {
                    // android phone tilt movement
                    vel.x = Input.acceleration.x * m_speed;
                }
                else
                {
                    // joystick, d-pad, left & right, A & D
                    vel.x = Input.GetAxisRaw("Horizontal") * m_speed;
                }
                break;
            default:
                // joystick, d-pad, left & right, A & D
                vel.x = Input.GetAxisRaw("Horizontal") * m_speed; 
                break;
        }

        rb.velocity = vel;

        //Shooting, also don't shoot if we're 'reloading'
        if (Input.GetAxisRaw("Fire1") == 1 && m_timer >= m_fireDelay)
        {
            //Reset timer
            m_timer = 0.0f;
            //fire bullet
            Fire(0.0f, m_bulletSpeed);
        }

        //Critical health flashing
        if (m_criticalState)
        {
            //Increase critical state timer
            m_critTimer += Time.deltaTime;
            //Player is currently not flash color and timer has reached limit
            if (!m_flash && m_critTimer >= m_flashDelay)
            {
                GetComponent<Renderer>().material.color = m_color;
                //Reset timer and toggle flash
                m_critTimer = 0.0f;
                m_flash = true;
            }
            //Player is currently flash color and timer has reached limit
            else if (m_flash && m_critTimer >= m_flashDuration)
            {
                GetComponent<Renderer>().material.color = m_originalColor;
                //Reset timer and toggle flash
                m_critTimer = 0.0f;
                m_flash = false;
            }
        }
	}

    private void Fire(float _x, float _y)
    {
        //Dont shoot if we dont have bullets
        if (m_poolReady.Count == 0)
            return;
        //Top of the stack will get fired
        GameObject top = m_poolReady.Pop();
        Rigidbody2D topRigid = top.GetComponent<Rigidbody2D>();
        //Set its position to ours plus the offset
        top.transform.position = transform.position + new Vector3(m_bulletOffset.x, m_bulletOffset.y);
        topRigid.velocity = new Vector2(_x, _y);
        //Set its speed
        top.GetComponent<PlayerBullet>().m_speed = new Vector2(_x, _y);
        //Activate it
        top.SetActive(true);

        // total shots fired += 1
        ScoreManager.AddFiredShot();
    }

    public void DestroyBullet(GameObject _bullet)
    {
        //If the bullet is active, deactivate it
        if (_bullet.activeSelf)
        {
            _bullet.SetActive(false);
            //Move it somewhere far away incase unity decides to do something weird with it
            _bullet.transform.position = new Vector3(-999.0f, -999.0f);
            //Push it back into the stack
            m_poolReady.Push(_bullet);
        }
    }
    
    /// <summary>
    /// handles the deduction of lives when objects collide with the player
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if player collides with an enemies bullet...
        if (collision.CompareTag("EnemyBullet"))
        {
            ScoreManager.LoseLife(); // lose one life

            //Critical health reached
            if (ScoreManager.GetLives() == 0)
                m_criticalState = true;
        }

        // if player collides with an enemy...
        if (collision.CompareTag("Enemy"))
        {
            ScoreManager.InstantDeath(); // instant death
        }
    }
}
