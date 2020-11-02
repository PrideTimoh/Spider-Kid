using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    PlayerScript player;

	void Start () 
	{
        SetInit();
	}
	
	

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player.lastCheckPoint = transform;
        }
    }

    void SetInit()
	{
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
	}
}
