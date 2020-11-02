using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelTimer : MonoBehaviour {

    public static LevelTimer instance;


    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        SetInit();

    }

    GameObject player;
    Slider timerSlider;

    public float time = 50f;
    public float maxTime;
    public bool timeUp;

    public float timeBurn = 1f;

	
	void Update () 
	{
		if(!player)
        {
            return;
        }
        if (time > maxTime)
        {
            time = maxTime;
        }
        if (time > 0)
        {
            time -= timeBurn * Time.deltaTime;
            timerSlider.value = time;
        }
        else if(time  <= 0)
        {
            timeUp = true;
            MusicManager.instance.musicEnabled = false;
            GameplayController.instance.TimeUp();
            //player.GetComponent<PlayerScript>().TimeFinished();
            if (MusicManager.instance.gameOverSound != null)
            {
                MusicManager.instance.PlaySound(MusicManager.instance.gameOverSound, 1);
            }
           
        }
	}

	void SetInit()
	{
        player = GameObject.Find("Player");
        timerSlider = GameObject.Find("Timer Slider").GetComponent<Slider>();
        timeUp = false;
        timerSlider.minValue = 0;
        timerSlider.maxValue = maxTime;
        time = maxTime;
        timerSlider.value = time;
	}
}
