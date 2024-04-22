using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayersManager : Singleton<PlayersManager>
{
    public event Action onPlayersDeath; 
    public event Action<Character> onPlayerHit; 
    
    [Header("Players")]
    [SerializeField] private Character bigPlayerPrefab;
    [SerializeField] private Character smallPlayerPrefab;

    private Character bigPlayer;
    private Character smallPlayer;
    
    public void SpawnPlayers()
    {
        LevelController levelController = FindObjectOfType<LevelController>();
        bigPlayer = Instantiate(bigPlayerPrefab, levelController.BigPlayerPosition, Quaternion.identity, levelController.transform);
        smallPlayer = Instantiate(smallPlayerPrefab, levelController.SmallPlayerPosition, Quaternion.identity, levelController.transform);
        
        bigPlayer.onPlayersDeath += HandlePlayerDeath;
        bigPlayer.onPlayerHit += NotifyPlayerHit;
        smallPlayer.onPlayersDeath += HandlePlayerDeath;
        smallPlayer.onPlayerHit += NotifyPlayerHit;
    }
    
    public void ForceStopPlayersMovement()
    {
        smallPlayer.Movement.ForceStopPlayer();
        bigPlayer.Movement.ForceStopPlayer();
    }
    
    public void ForceStartPlayersMovement()
    {
        bigPlayer.Movement.ForceStartPlayer();
        smallPlayer.Movement.ForceStartPlayer();
    }
    
    private void NotifyPlayerHit(Character playerType)
    {
       onPlayerHit?.Invoke(playerType);
    }

    private void HandlePlayerDeath(Character character)
    {
        onPlayersDeath?.Invoke();
    }
    
    //TODO Add after passing/ not passing level
    void DestroyPlayers(Character character) {
        bigPlayer.onPlayersDeath -= HandlePlayerDeath;
        bigPlayer.onPlayerHit -= NotifyPlayerHit;
        smallPlayer.onPlayersDeath -= HandlePlayerDeath;
        smallPlayer.onPlayerHit -= NotifyPlayerHit;
        
        Destroy(character.gameObject);
    }
}
