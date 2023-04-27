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
        Lava lava = col.GetComponent<Lava>();

        if (spikes)
        {
            DecreaseHealth();
        }
        if (lava)
        {
            while(health > 0)
            {
                DecreaseHealth();
            }
        }
    }

    private void DecreaseHealth()
    {
        if (--health == 0)
        {
            onPlayersDeath?.Invoke();
        }
        gameObject.GetComponent<Animator>().SetTrigger("attack");
        onPlayerHit?.Invoke();
    }
}
