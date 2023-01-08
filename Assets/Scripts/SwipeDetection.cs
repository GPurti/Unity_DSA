using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    private Vector2 startPos;
    public int pixelDistToDetect = 50;
    private bool fingerDown;

    void Update()
    {
        /*
        if(fingerDown == false && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            startPos = Input.touches[0].position;
            fingerDown = true;
        }
        
        if (fingerDown)
        {
            //Swiping up
            if(Input.touches[0].position.y >= startPos.y + pixelDistToDetect)
            {
                fingerDown = false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().MovePhone(Vector3.up);
            } 
            else if (Input.touches[0].position.x <= startPos.x - pixelDistToDetect)
            {
                fingerDown = false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().MovePhone(Vector3.left);
            }
            else if (Input.touches[0].position.x >= startPos.x + pixelDistToDetect)
            {
                fingerDown = false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().MovePhone(Vector3.right);
            }
            else if (Input.touches[0].position.y <= startPos.y - pixelDistToDetect)
            {
                fingerDown = false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().MovePhone(Vector3.down);
            }
        }

        if (fingerDown && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
        {
            fingerDown = false;
        }*/

        //TESTING FOR PC IN UNITY
        
        if (fingerDown == false && Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            fingerDown = true;
        }
        
        if (fingerDown)
        {
            if (Input.mousePosition.y >= startPos.y + pixelDistToDetect)
            {
                fingerDown = false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().MovePhone(Vector3.up);
            }
            else if (Input.mousePosition.x <= startPos.x - pixelDistToDetect)
            {
                fingerDown = false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().MovePhone(Vector3.left);
            }
            else if (Input.mousePosition.x >= startPos.x + pixelDistToDetect)
            {
                fingerDown = false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().MovePhone(Vector3.right);
            }
            else if (Input.mousePosition.y <= startPos.y - pixelDistToDetect)
            {
                fingerDown = false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().MovePhone(Vector3.down);
            }
        }
        
        if(fingerDown && Input.GetMouseButtonUp(0))
        {
            fingerDown = false;
        }
        
    }
}
