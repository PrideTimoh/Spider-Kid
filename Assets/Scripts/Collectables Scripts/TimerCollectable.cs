using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerCollectable : MonoBehaviour {

    LevelTimer timer;

    private void Awake()
    {
        timer = GameObject.Find("Gameplay Controller").GetComponent<LevelTimer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(timer.time == timer.maxTime)
            {
                if (MusicManager.instance.refuseCollectSound != null)
                {
                    MusicManager.instance.PlaySound(MusicManager.instance.refuseCollectSound, .4f);
                }
                Debug.Log("Time Already Full");
            }
            else
            {
                if (MusicManager.instance.timerCollectSound != null)
                {
                    MusicManager.instance.PlaySound(MusicManager.instance.timerCollectSound, 1);
                }
                timer.time += 15f;
                Destroy(gameObject);
            }
           
        }
    }

}
