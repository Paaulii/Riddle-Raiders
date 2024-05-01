using System;
using System.Collections;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public Action onSpikeHit;
    public Action onPlayersDeath;
    public Action onPlayerHit;

    [SerializeField] private float decreaseHealthOverTimeInterval;
    [SerializeField] private int health;
    [SerializeField] private HitDetector hitDetector;

    private bool healthDecreasing;
    private WaitForSeconds decreaseHealthInterval;
    
    private void Start()
    {
        decreaseHealthInterval = new WaitForSeconds(decreaseHealthOverTimeInterval);
        hitDetector.onSpikeHit += HandleSpikeHit;
        hitDetector.onLavaEnter += StartDecreasingHealth;
        hitDetector.onLavaExit += StopDecreasingHealth;
    }

    private void OnDestroy()
    {
        hitDetector.onSpikeHit -= HandleSpikeHit;
        hitDetector.onLavaEnter -= StartDecreasingHealth;
        hitDetector.onLavaExit -= StopDecreasingHealth;
    }

    private void HandleSpikeHit()
    {
        onSpikeHit?.Invoke();
        DecreaseHealth();
    }
    
    private void StopDecreasingHealth()
    {
        healthDecreasing = false;
    }

    private void StartDecreasingHealth()
    {
        healthDecreasing = true;
        StartCoroutine(DecreaseHealthOverTime());
    }

    private void DecreaseHealth()
    {
        health--;
        VerifyHit();
    }

    private void VerifyHit()
    {
        if (health == 0)
        {
            Destroy(gameObject);
            onPlayersDeath?.Invoke();
        }
        gameObject.GetComponent<Animator>().SetTrigger("attack");
        onPlayerHit?.Invoke();
    }
    
    //TODO: add effect based on kind of death
    private IEnumerator DecreaseHealthOverTime()
    {
        while (healthDecreasing && health > 0)
        {
            DecreaseHealth();
            yield return decreaseHealthInterval;
        }
    }
}
