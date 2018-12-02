using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseDoor : MonoBehaviour {

    private int playersInDoor;
    private SpriteRenderer rend;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    private void HideDoor()
    {
        if(++playersInDoor > 0)
        {
            rend.enabled = false;
        }
    }

    private void ShowDoor()
    {
        if(--playersInDoor <= 0)
        {
            rend.enabled = true;
            playersInDoor = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            ShowDoor();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            HideDoor();
        }
    }

}
