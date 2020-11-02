using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncy : MonoBehaviour {

    public float bounceForce = 500;
    PlayerScript player;

    Animator myAnim;


	void Awake () 
	{
        SetInit();
	}
	
    IEnumerator BouncePlayer()
    {
        myAnim.Play("Up");
        yield return new WaitForSeconds(.5f);
        myAnim.Play("Down");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerScript>().BouncePlayerWithBouncy(bounceForce);
            StartCoroutine(BouncePlayer());
        }
    }
   

	void SetInit()
	{
        myAnim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
	}
}
