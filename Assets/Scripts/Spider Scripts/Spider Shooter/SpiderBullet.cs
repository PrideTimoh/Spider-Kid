using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBullet : MonoBehaviour {


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (MusicManager.instance.hurtSound != null)
            {
                MusicManager.instance.PlaySound(MusicManager.instance.hurtSound, 1);
            }
            GameObject.Find("Player").GetComponent<PlayerScript>().DeductHealth(15f);
            Destroy(gameObject);
        }
        else if(collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

}
