using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelHandlerState : State
{
    protected virtual void OnLoadingLevelComplete() { }
    protected virtual void OnUnloadingLevelComplete() { }

    protected IEnumerator LoadSceneAsync(string levelName)
    {
        //TODO: add screen loader
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        OnLoadingLevelComplete();
    }
    
    protected IEnumerator UnloadSceneAsync(string levelName)
    {
        //TODO: add screen loader
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(levelName);

        while (!asyncUnload.isDone)
        {
            yield return null;
        }

        OnUnloadingLevelComplete();
    }
}
