using UnityEngine;
using UnityEngine.UI;

public class CollectedStarsDisplay : MonoBehaviour
{
    [SerializeField] private Image starImage;
    [SerializeField] private Sprite[] stars;

    int currentStarAmount;
    
    private void Start() 
    {
        ResetStars();
    }
    
    public void IncreaseStars()
    {
        currentStarAmount++;
        UpdateDisplayedStars();
    }
    
    public void ResetStars()
    {
        currentStarAmount = 0;
        UpdateDisplayedStars();
    }

    private void UpdateDisplayedStars()
    {
        starImage.sprite = stars[currentStarAmount];
    }
}
