using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DoorScript : MonoBehaviour {


    GameObject[] diamonds;
    public GameObject diamondReminderPanel;
    int reminderDelay = 2;


    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        SetInit();
        

    }

    public static DoorScript instance;

    public Text totalDiamonds;
    public Text collectedDiamonds;
    public bool playerWon;
  

    Collider2D myCollider;
    Animator myAnim;

    
    public int collectablesCount;
    public int collectedCollectablesCount;



    private void Start()
    {

        StartCoroutine(ShowDiamonds());
    }

    private void Update()
    {
        
        collectedDiamonds.text = collectedCollectablesCount.ToString();
        
    }
    IEnumerator ShowDiamonds()
    {
        yield return new WaitForSeconds(0f);
        totalDiamonds.text = collectablesCount.ToString();
    
    }


    IEnumerator OpenDoor()
    {
        myAnim.Play("Door_Open");
        yield return new WaitForSeconds(.7f);
        myCollider.isTrigger = true;

    }
    public void DecrementCollectibles()
    {
        collectablesCount--;
        collectedCollectablesCount++;
        if(collectablesCount == 0)
        {
            StartCoroutine(OpenDoor());
        }
    }

    IEnumerator ShowDiamondReminder()
    {
        diamondReminderPanel.SetActive(true);
        yield return new WaitForSeconds(reminderDelay);
        diamondReminderPanel.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == ("Player"))
        {
            if(!playerWon)
            {
                StartCoroutine(ShowDiamondReminder());
            }
           
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            MusicManager.instance.musicEnabled = false; ;
            playerWon = true;
            GameplayController.instance.PLayerWon();

            if (MusicManager.instance.winSound != null)
            {
                MusicManager.instance.PlaySound(MusicManager.instance.winSound, 1);
            }

        }
    }

    

	void SetInit()
	{
        diamonds = GameObject.FindGameObjectsWithTag("Diamond");
        collectablesCount = diamonds.Length;
        playerWon = false;
        myCollider = GetComponent<BoxCollider2D>();
        myAnim = GetComponent<Animator>();
    }
}
