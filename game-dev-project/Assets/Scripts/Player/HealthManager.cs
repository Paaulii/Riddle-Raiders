using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HealthManager : MonoBehaviour
{
    public Action onPlayersDeath;
    public Action onPlayerHit;
    
    [Header("Health value")]
    private int health = 3;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        Spikes spikes = col.GetComponent<Spikes>();

        if (spikes)
        {
            DecreaseHeath();
        }
    }

    private void DecreaseHeath()
    {
        if (--health == 0)
        {
            onPlayersDeath?.Invoke();
        }

        onPlayerHit?.Invoke();
    }
}
