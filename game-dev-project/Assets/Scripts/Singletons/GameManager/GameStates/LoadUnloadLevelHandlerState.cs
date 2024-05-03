using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadUnloadLevelHandlerState : State
{
    protected virtual void OnLoadingLevelComplete() { }
    protected virtual void OnUnloadingLevelComplete() { }
    protected void UnloadScene(string sceneName)
    {
        if (!IsSceneValid(sceneName))
        {
            return;
        }
        
        StartCoroutine(UnloadSceneAsync(sceneName));
    }

    protected void LoadScene(string sceneName)
    {
        if (!IsSceneValid(sceneName))
        {
            return;
        }
        
        StartCoroutine(LoadSceneAsync(sceneName));
    }
    
    IEnumerator LoadSceneAsync(string levelName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        OnLoadingLevelComplete();
    }
    
    IEnumerator UnloadSceneAsync(string levelName)
    {
        //TODO: add screen loader
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(levelName);

        while (!asyncUnload.isDone)
        {
            yield return null;
        }

        OnUnloadingLevelComplete();
    }

    private bool IsSceneValid(string sceneName)
    {
        return SceneUtility.GetBuildIndexByScenePath(sceneName) != -1;
    }
}
