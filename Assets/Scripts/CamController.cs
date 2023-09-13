using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
     public Transform player;
     public Vector3 offsetPosition;
     public Space offsetPositionSpace = Space.Self;
     public bool lookAt = true;
     public bool gameStarted;


     private void Update()
     {
        Refresh();
     }

     public void Refresh()
     {
        if(player == null)
        {
            return;
        }


        if(offsetPositionSpace == Space.Self)
        {
            transform.position = player.TransformPoint(offsetPosition);
        }
        else
        {
            transform.position = player.position + offsetPosition;
        }


        if(lookAt)
        {
            transform.LookAt(player);
        }
        else
        {
            transform.rotation = player.rotation;
        }

     }
/*
     public void StartGame()
     {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            FindAnyObjectByType<CharController>().StartRunning();
        }
     }  
     */
}
