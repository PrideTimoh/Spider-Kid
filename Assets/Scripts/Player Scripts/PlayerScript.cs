using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public static PlayerScript instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public Transform lastCheckPoint;
    public Text Respawns;
    public int respawnNumber = 3;
    public GameObject respawnPanel;

    private Animator myAnim;
    private Rigidbody2D myRigidbody;
    Button jumpButton;
    public float maxSpeed = 4f;
    public float moveForce = 50f;
    public float jumpForce =700f;

    public bool Grounded;
    public bool moveLeft, moveRight;

    public float health;
    Slider healthSlider;
    public float minHealth;
    public float maxHealth;

    void OnEnable () 
	{
       
        SetInit();
        jumpButton.onClick.AddListener(() => Jump());
	}
	
	void Update () 
	{
        Respawns.text = respawnNumber.ToString();
        healthSlider.value = health;
        CheckForPlayerDead();
        MovePlayer();
        //MovePlayerWithJoystick();
	}

    void SetInit()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        jumpButton = GameObject.Find("Jump Button").GetComponent<Button>();
        healthSlider = GameObject.Find("Health Slider ").GetComponent<Slider>();

        healthSlider.minValue = minHealth;
        healthSlider.maxValue = maxHealth;
        health = maxHealth;
        healthSlider.value = health;
    }

    public void IncreaseHealth(float increase)
    {
        health += increase;
        if(health > maxHealth)
        {
            health = maxHealth;
        }

        
    }
    public void DeductHealth(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            if (respawnNumber > 0)
            {
               RespawnPlayer();
            }
            else
            {
                CheckForPlayerDead();
            }

        }

    }
    void ActivateRSPDelay()
    {
        respawnPanel.SetActive(false);
    }
    void ActivatePlayerDelay()
    {
        gameObject.SetActive(true);
    }

    void RespawnPlayer()
    {
        MusicManager.instance.musicEnabled = false;

        gameObject.SetActive(false);
        Time.timeScale = 0;
        respawnPanel.SetActive(true);
        health = maxHealth;
        transform.position = lastCheckPoint.position;
        Invoke("ActivateRSPDelay", 2f);
       
        Invoke("ActivatePlayerDelay", 2f);

        Time.timeScale = 1;

        respawnNumber--;
        MusicManager.instance.musicEnabled = true;

    }

    public void PlayerDie()
    {
        GameObject.Find("Gameplay Controller").GetComponent<GameplayController>().PlayerDied();
        Destroy(gameObject);
    }

    public IEnumerator KillPlayer()
    {
        yield return new WaitForSeconds(0f);
        if (MusicManager.instance.gameOverSound != null)
        {
            MusicManager.instance.PlaySound(MusicManager.instance.gameOverSound, 1);
        }

        GameplayController.instance.PlayerDied();
        Destroy(gameObject);
    }

   

    //public void TimeFinished()
    //{
    //    MusicManager.instance.musicEnabled = false;
      
    //    GameObject.Find("Gameplay Controller").GetComponent<GameplayController>().TimeUp();

    //}


    void CheckForPlayerDead()
    {
       
        
        if (health <= minHealth)
        {
            StartCoroutine(KillPlayer());
        }
       
       
    }

    public void SetMoveLeft(bool moveLeft)
    {

        this.moveLeft = moveLeft;
        this.moveRight = !moveLeft;
    }
   

    public void StopMoving()
    {
        this.moveLeft = false;
        this.moveRight = false;
    }

    void MovePlayerWithJoystick()
    {
        float forceX = 0f;
        float vel = Mathf.Abs(myRigidbody.velocity.x);
        if (moveRight)
        {
            if (vel < maxSpeed)
            {
                if (Grounded)
                {
                    //if (MusicManager.instance.moveSound != null)
                    //{
                    //    MusicManager.instance.PlaySound(MusicManager.instance.moveSound, 1.5f);
                    //}
                    forceX = moveForce;
                }
                else
                {
                    forceX = moveForce * 1.1f;
                }
            }
            GetComponent<SpriteRenderer>().flipX = false;

            myAnim.SetBool("Walk", true);
       }
       else if(moveLeft)
       {
           
            if (vel < maxSpeed)
            {
                if (Grounded)
                {
                    //if (MusicManager.instance.moveSound != null)
                    //{
                    //    MusicManager.instance.PlaySound(MusicManager.instance.moveSound, 1.5f);
                    //}
                    forceX = -moveForce;
                }
                else
                {
                    forceX = -moveForce * 1.1f;
                }
            }
            GetComponent<SpriteRenderer>().flipX = true;

            myAnim.SetBool("Walk", true);
       }
        else
        {
            myAnim.SetBool("Walk", false);
        }

        myRigidbody.AddForce(new Vector2(forceX, 0));
    }
    void Jump()
    {
        if(GameplayController.instance.isPaused)
        {
            return;
        }
        if (Grounded)
        {
            if (MusicManager.instance.jumpSound != null)
            {
                MusicManager.instance.PlaySound(MusicManager.instance.jumpSound, 2);
            }
            
            Grounded = false;
            myRigidbody.AddForce(new Vector2(0, jumpForce));
        }
    }

    void MovePlayer()
    {
        float forceX = 0f;
        float forceY = 0f;

        float vel = Mathf.Abs(myRigidbody.velocity.x);

        float h = Input.GetAxisRaw("Horizontal");
       
            if (Input.GetKeyDown(KeyCode.Space))
            {
                  if (GameplayController.instance.isPaused)
                  {
                     return;
                  }

                if (Grounded)
                {
                   if (MusicManager.instance.jumpSound != null)
                   {
                    MusicManager.instance.PlaySound(MusicManager.instance.jumpSound, 2f);
                   }
                      Grounded = false;
                    forceY = jumpForce;
                }
                 
   
            }

        if (h > 0)
        {
            if (vel < maxSpeed)
            {
                if(Grounded)
                {
                    //if (MusicManager.instance.moveSound != null)
                    //{
                    //    MusicManager.instance.PlaySound(MusicManager.instance.moveSound, 1.5f);
                    //}
                    forceX = moveForce;
                }
                else
                {
                    forceX = moveForce / 1.2f;
                }
            }
            GetComponent<SpriteRenderer>().flipX = false;

            myAnim.SetBool("Walk", true);

        }
        else if (h < 0)
        {
            if (vel < maxSpeed)
            {
                if (Grounded)
                {
                    //if (MusicManager.instance.moveSound != null)
                    //{
                    //    MusicManager.instance.PlaySound(MusicManager.instance.moveSound, 1);
                    //}
                    forceX = -moveForce;
                }
                else
                {
                    forceX = -moveForce / 1.2f;
                }
            }
            GetComponent<SpriteRenderer>().flipX = true;

            myAnim.SetBool("Walk", true);

        }
        else if (h == 0)
        {
            myAnim.SetBool("Walk", false);
        }

       
        myRigidbody.AddForce(new Vector2(forceX, forceY));
    }

    public void BouncePlayerWithBouncy(float force)
    {
        myRigidbody.AddForce(new Vector2(0, force));
        //if (Grounded)
        //{
        //    Grounded = false;
           
        //}
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            Grounded = true;
        }
    }
   
}
