using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour 
{
    public Action onStartCollect;
    private void OnTriggerEnter2D(Collider2D col)
    {
        Character character = col.GetComponent<Character>();

        if (character)
        {
            onStartCollect?.Invoke();
            Destroy(gameObject);
        }
    }
}
