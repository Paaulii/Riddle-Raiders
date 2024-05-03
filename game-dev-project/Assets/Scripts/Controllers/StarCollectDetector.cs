using System;
using UnityEngine;

public class StarCollectDetector : MonoBehaviour
{
    public int StarsAmount = 0;
    public Action<Vector2> onStarCollected;
    private Star[] stars;
    
    public void FindAllStarsAtLevel()
    {
        Deinit();
        stars = FindObjectsOfType<Star>(true);
        foreach (var star in stars)
        {
            star.onStarCollect += HandleStarCollection;
        }
    }
    private void Deinit()
    {
        StarsAmount = 0;
        if (stars == null || stars?.Length == 0)
        {
            return;
        }
        
        foreach (var star in stars)
        {
            star.onStarCollect -= HandleStarCollection;
        }
    }

    private void HandleStarCollection(Star collectedStar) {
        StarsAmount++;
        onStarCollected?.Invoke(collectedStar.transform.position);
        collectedStar.onStarCollect -= HandleStarCollection;
    }
}
