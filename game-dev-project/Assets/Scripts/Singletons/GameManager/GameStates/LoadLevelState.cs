using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelState : State
{
    public override void OnEnter()
    {
        LoadLastLevel();
    }

    private void LoadLastLevel()
    {
        string levelName = SaveSystemManager.instance.GetLastLevelName();

        if (levelName == default)
        {
            return;
        }

        StartCoroutine(LoadSceneAsync(levelName));
    }
    
    IEnumerator LoadSceneAsync(string levelName)
    {
        //TODO: add screen loader
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        PlayersManager.instance.SpawnPlayers();
        GameManager.Instance.Data.Status = GameData.GameStatus.InLevel;
    }
}

