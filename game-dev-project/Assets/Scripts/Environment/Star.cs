using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour 
{
    public Action<Star> onStarCollect;
    [SerializeField] private GameObject pickupStartEffect;
    private void OnTriggerEnter2D(Collider2D col)
    {
        Character character = col.GetComponent<Character>();

        if (character)
        {
            Instantiate(pickupStartEffect, transform.position, Quaternion.identity);
            onStarCollect?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
