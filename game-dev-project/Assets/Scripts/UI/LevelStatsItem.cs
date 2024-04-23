using TMPro;
using UnityEngine;

public class LevelStatsItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI completionTimeText;
    [SerializeField] private CollectedStarsDisplayer starsCollectedDisplayer;

    public void Setup(string completionTime, int collectedStars)
    {
        starsCollectedDisplayer.SetCollectedStars(collectedStars);
        completionTimeText.text = completionTime;
    }
}
