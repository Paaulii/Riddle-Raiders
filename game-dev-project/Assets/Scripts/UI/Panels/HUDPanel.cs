using UnityEngine;

public class HUDPanel : Panel
{
    [SerializeField] private HUDController controller;
    
    public void SetLevelNumber(int levelNumber)
    {
        controller.SetLevelNumber(levelNumber);
    }
    
    public void UpdateTime(string time)
    {
        controller.UpdateTime(time);
    }
    
    public void IncreaseStarAmount()
    {
        controller.IncreaseStarAmount();
    }

    public void DecreasePlayersHealth(Character.CharacterType characterType)
    {
        controller.DecreasePlayersHealth(characterType);
    }

    public void ResetStarsDisplayed()
    {
        controller.ResetCollectedStars();
    }
}
