using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Bson;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Players")]
    [SerializeField] private Character bigPlayer;
    [SerializeField] private Character smallPlayer;

    [Header("UI objects")] 
    [SerializeField] private UIController uiController;

    private void Start()
    {
        BindToEvents();
    }

    private void OnDestroy()
    {
        UnbindFromEvents();
    }

    private void BindToEvents()
    {
        bigPlayer.onPlayersDeath += HandleGameOverState;
        bigPlayer.onPlayerHit += DecreaseUIHearts;
        
        smallPlayer.onPlayersDeath += HandleGameOverState;
        smallPlayer.onPlayerHit += DecreaseUIHearts;
    }
    
    private void UnbindFromEvents()
    {
        bigPlayer.onPlayersDeath -= HandleGameOverState;
        bigPlayer.onPlayerHit -= DecreaseUIHearts;
        
        smallPlayer.onPlayersDeath -= HandleGameOverState;
        smallPlayer.onPlayerHit -= DecreaseUIHearts;
    }
    
    private void DecreaseUIHearts(Character character)
    {
        uiController.DecreasePlayersHealth(character.Type);
    }
    
    private void HandleGameOverState()
    {
        uiController.SetActiveGameOverText(true);
        bigPlayer.Movement.enabled = false;
        smallPlayer.Movement.enabled = false;
    }
    
    private IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
