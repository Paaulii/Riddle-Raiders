public class LevelSelectionState : State
{
   private LevelSelectionPanel panel;
   public override void OnEnter()
   {
      base.OnEnter();
      ShowLevelSelectionPanel();
      InitLevelGrid();
   }

   public override void OnExit()
   {
      base.OnExit();
      panel.onLevelSelected -= HandleLevelSelected;
      panel.onResetData -= ResetData;
      panel.onReturnToMenu -= HandleReturnToMenu;
   }

   private void ShowLevelSelectionPanel()
   {
      panel = PanelManager.instance.ShowPanel<LevelSelectionPanel>();
      panel.onLevelSelected += HandleLevelSelected;
      panel.onResetData += ResetData;
      panel.onReturnToMenu += HandleReturnToMenu;
   }

   private void InitLevelGrid()
   {
      PlayerGameProgress gameProgress = SaveSystemManager.instance.LoadData();
      panel.InitGridForData(gameProgress.LevelsData);
   }

   private void HandleLevelSelected(int levelNumber)
   {
      GameManager.Instance.Data.CurrentLevel = SaveSystemManager.instance.GetLevelByIndex(levelNumber);
      GameManager.Instance.Data.Status = GameData.GameStatus.LoadLevel;
   }
   
   private void ResetData()
   {
      SaveSystemManager.instance.ResetData();
      InitLevelGrid();
   }
   
   private void HandleReturnToMenu()
   {
      GameManager.Instance.Data.Status = GameData.GameStatus.MainMenu;
   }
}
