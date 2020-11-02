using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderShooter : MonoBehaviour {

    public GameObject Bullet;
    //public float minSeconds;
    //public float maxSeconds;


	void Start () 
	{
        
        StartCoroutine(Shoot());

    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(Random.Range(0, 7));
        Instantiate(Bullet, transform.position, Quaternion.identity);
        StartCoroutine(Shoot());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (MusicManager.instance.hurtSound != null)
            {
                MusicManager.instance.PlaySound(MusicManager.instance.hurtSound, 1);
            }
            GameObject.Find("Player").GetComponent<PlayerScript>().DeductHealth(10f); ;

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (MusicManager.instance.hurtSound != null)
            {
                MusicManager.instance.PlaySound(MusicManager.instance.hurtSound, 1);
            }
            GameObject.Find("Player").GetComponent<PlayerScript>().DeductHealth(10f); ;

        }
    }


}
