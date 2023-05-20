using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StarCollectDetector : MonoBehaviour
{
    public int StarsAmount = 0;
    public Action onStarCollected;
    
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip collectSound;
    
    private Star[] stars;
    void Start()
    {
        stars = GetComponentsInChildren<Star>(true);
        foreach (var star in stars)
        {
            star.onStartCollect += HandleStarCollection;
        }
    }

    private void OnDestroy()
    {
        foreach (var star in stars)
        {
            star.onStartCollect -= HandleStarCollection;
        }
    }

    private void HandleStarCollection() {
        audioSource.PlayOneShot(collectSound);
        StarsAmount++;
        onStarCollected?.Invoke();
    }
}
