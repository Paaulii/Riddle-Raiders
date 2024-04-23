using UnityEngine;

public class HUDPanel : Panel
{
    [SerializeField] private HUDController controller;

    public override void Show()
    {
        base.Show();
        controller.SetLevelNumber(GameManager.Instance.Data.CurrentLevel.LevelNumber + 1);
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
}
