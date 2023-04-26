using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterEndDoor : MonoBehaviour
{
    public Action onEnterEndDoor;

    private bool smallPlayerAtDoor = false;
    private bool bigPlayerAtDoor = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Small Player")
        {
            smallPlayerAtDoor = true;
        }
        else if (col.name == "Big Player")
        {
            bigPlayerAtDoor = true;
        }

        if (smallPlayerAtDoor && bigPlayerAtDoor)
        {
            onEnterEndDoor?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.name == "Small Player")
        {
            smallPlayerAtDoor = false;
        }
        else if (col.name == "Big Player")
        {
            bigPlayerAtDoor = false;
        }
    }
}
