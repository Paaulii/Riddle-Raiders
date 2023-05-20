using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPanelController : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Button startGameButton;
    [SerializeField] private UnityEngine.UI.Button chooseLevelButton;
    [SerializeField] private UnityEngine.UI.Button creditsButton;
    [SerializeField] private UnityEngine.UI.Button exitGameButton;
    
    [Space]
    
    [SerializeField] private DataManager dataManager;
    private void Start()
    {
        startGameButton.onClick.AddListener(() =>
        {
            LoadLastLevel();
        });
        
        chooseLevelButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("LevelSelection");
        });
        
        creditsButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Credits");
        });
        
        exitGameButton.onClick.AddListener(() =>
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else 
                Application.Quit();
            #endif
            
        });
    }

    private void LoadLastLevel()
    {
        PlayerGameProgress playerGameProgress = dataManager.LoadData();
        LevelData lastPlayedLevel = playerGameProgress.LevelsData.FindLast(level => level.IsLocked == false);

        if (lastPlayedLevel != null)
        {
            SceneManager.LoadScene(lastPlayedLevel.PathToScene);
        }
    }
}
