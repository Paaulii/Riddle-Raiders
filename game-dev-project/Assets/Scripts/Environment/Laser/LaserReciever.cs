using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class LaserReciever : MonoBehaviour
{
    public bool isActive = false;
    
    void Update() {
        if (isActive) 
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else 
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }

        isActive = false;
    }
}
