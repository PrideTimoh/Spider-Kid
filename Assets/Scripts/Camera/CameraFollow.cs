using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public float minX, maxX;
    public float followSpeed;
    public Transform player;
    public Vector3 offset;
    Vector3 desiredPos;
    Vector3 smoothPos;


    private void Awake()
    {
        SetInit();
    }

    private void LateUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        if(player != null)
        {
            desiredPos = player.position + offset;
            if (desiredPos.x < minX)
            {
                desiredPos.x = minX;
            }
            else if (desiredPos.x > maxX)
            {
                desiredPos.x = maxX;
            }
            smoothPos = Vector3.Lerp(transform.position, desiredPos, followSpeed * Time.deltaTime);

            transform.position = smoothPos;
        }
       
    }

    void SetInit()
    {
       // player = GameObject.Find("Player").GetComponent<Transform>();
    }









}
