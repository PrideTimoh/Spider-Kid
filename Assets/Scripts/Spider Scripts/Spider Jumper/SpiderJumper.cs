using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderJumper : MonoBehaviour {

    private Rigidbody2D myRigidbody;
    private Animator myAnim;

    //public float minSeconds;
    //public float maxSeconds;
    public float forceY;

    private void Awake()
    {
        SetInit();
    }
    void Start () 
	{
        StartCoroutine(Attack());
	}

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(Random.Range(0, 7));
        forceY = Random.Range(250f, 550f);

        myRigidbody.AddForce(new Vector2(0, forceY));
        myAnim.SetBool("Attack", true);

        yield return new WaitForSeconds(.7f);
        StartCoroutine(Attack());

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ground"))
        {
            myAnim.SetBool("Attack", false);
        }
        else if (collision.CompareTag("Player"))
        {
            if (MusicManager.instance.hurtSound != null)
            {
                MusicManager.instance.PlaySound(MusicManager.instance.hurtSound, 1);
            }
            GameObject.Find("Player").GetComponent<PlayerScript>().DeductHealth(25f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            myAnim.SetBool("Attack", false);
        }
        else if (collision.CompareTag("Player"))
        {
            if (MusicManager.instance.hurtSound != null)
            {
                MusicManager.instance.PlaySound(MusicManager.instance.hurtSound, 1);
            }
            GameObject.Find("Player").GetComponent<PlayerScript>().DeductHealth(25f);
        }
    }


    void SetInit()
	{
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
	}
}
