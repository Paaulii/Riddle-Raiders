using UnityEngine.SceneManagement;

public class LoadLevelState : State
{
    public override void OnEnter() 
    {
        LoadLastLevel();
    }
    
    private void LoadLastLevel()
    {
        string levelName = SaveSystemManager.instance.GetLastLevelName() ;
        
        if (levelName == default)
        {
            return;
        }
        
        SceneManager.LoadScene(levelName);
        GameManager.Instance.Data.Status = GameData.GameStatus.InLevel;
    }
}
