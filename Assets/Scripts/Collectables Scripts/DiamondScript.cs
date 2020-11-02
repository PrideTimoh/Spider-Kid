using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondScript : MonoBehaviour {


  
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            
            if (MusicManager.instance.diamondCollectSound != null)
            {
                MusicManager.instance.PlaySound(MusicManager.instance.diamondCollectSound, 1);
            }
            if (DoorScript.instance != null)
            {
                DoorScript.instance.DecrementCollectibles();
                //if (gameObject.tag == ("Diamond"))
                //{
                  

                //}
            }
            Destroy(gameObject);
        }
    }

   
}
