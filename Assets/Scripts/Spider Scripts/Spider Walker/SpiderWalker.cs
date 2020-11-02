using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWalker : MonoBehaviour {

    [SerializeField]
    Transform startPos, endPos;
    bool colliding;
    public float speed = 1f;

    private Rigidbody2D myRigidbody;



    private void Awake()
    {
        SetInit();
    }
    void Start () 
	{
		
	}
	
	void FixedUpdate () 
	{
        MovePlayer();
        ChangeDirection();
	}

    void MovePlayer()
    {
        myRigidbody.velocity = new Vector2(transform.localScale.x, 0) * speed;
    }

    void ChangeDirection()
    {
        colliding = Physics2D.Linecast(startPos.position, endPos.position, 1 << LayerMask.NameToLayer("Ground"));
        Debug.DrawLine(startPos.position, endPos.position, Color.red);

        if (!colliding)
        {
            Vector3 temp = transform.localScale;
            if(temp.x == -1)
            {
                temp.x = 1;
            }
            else if (temp.x == 1)
            {
                temp.x = -1;
            }


            transform.localScale = temp;
        }
    }
    private void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {

            if (MusicManager.instance.hurtSound != null)
            {
                MusicManager.instance.PlaySound(MusicManager.instance.hurtSound, 1);
            }

            GameObject.Find("Player").GetComponent<PlayerScript>().DeductHealth(25f);

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.CompareTag("Player"))
        {
            
             if (MusicManager.instance.hurtSound != null)
             {
                    MusicManager.instance.PlaySound(MusicManager.instance.hurtSound, 1);
             }
            
            GameObject.Find("Player").GetComponent<PlayerScript>().DeductHealth(25f);

        }
        else if (collision.gameObject.CompareTag("Jumper"))
        {
            Vector3 temp = transform.localScale;
            if (temp.x == -1)
            {
                temp.x = 1;
            }
            else if (temp.x == 1)
            {
                temp.x = -1;
            }


            transform.localScale = temp;
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
           // Do Nothing
        }
        else
        {
            Vector3 temp = transform.localScale;
            if (temp.x == -1)
            {
                temp.x = 1;
            }
            else if (temp.x == 1)
            {
                temp.x = -1;
            }

        }
    }

    void SetInit()
	{
        myRigidbody = GetComponent<Rigidbody2D>();
     
    }
}
