using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectable : MonoBehaviour {

    PlayerScript player;
	void Awake () 
	{
        SetInit();
	}
	
	
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(player.health == player.maxHealth)
            {
                if (MusicManager.instance.refuseCollectSound != null)
                {
                    MusicManager.instance.PlaySound(MusicManager.instance.refuseCollectSound, .5f);
                }
                Debug.Log("Health Already Full");
            }
            else if(player.health <= player.maxHealth)
            {
                if (MusicManager.instance.healthCollectSound != null)
                {
                    MusicManager.instance.PlaySound(MusicManager.instance.healthCollectSound, 1);
                }
                player.IncreaseHealth(5f);
                Destroy(gameObject);
            }
            
        }
    }

    void SetInit()
	{
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
	}
}
