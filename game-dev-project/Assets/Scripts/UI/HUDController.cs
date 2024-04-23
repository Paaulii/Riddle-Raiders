using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] private PlayerStateDisplay smallPlayerStateDisplay;
    [SerializeField] private PlayerStateDisplay bigPlayerStateDisplay;
    [SerializeField] private AchievedStarDisplay achievedStarDisplay;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI timeText;
    
    public void UpdateTime(string time)
    {
        timeText.text = time;
    }
    
    public void DecreasePlayersHealth(Character.CharacterType characterType)
    {
        switch (characterType)
        {
            case Character.CharacterType.Big:
                bigPlayerStateDisplay.DecreaseHeartsAmount();
                break;
            case Character.CharacterType.Small:
                smallPlayerStateDisplay.DecreaseHeartsAmount();
                break;
        }
    }
    
    public void IncreaseStarAmount()
    {
        achievedStarDisplay.ChangeStarsNumber();
    }
    
    public void SetLevelNumber(int levelNumber)
    {
        levelText.text = "Level " + levelNumber;
    }
}
