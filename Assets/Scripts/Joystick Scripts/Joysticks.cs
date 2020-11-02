using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Joysticks : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private PlayerScript player;
    
    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
    }

    public void OnPointerDown(PointerEventData data)
    {
        if(this.name == "Left Button")
        {
            player.SetMoveLeft(true);
        }
        else if(this.name == "Right Button")
        {
            player.SetMoveLeft(false);
        }
    }

    public void OnPointerUp(PointerEventData data)
    {
        player.StopMoving();
    }

}
