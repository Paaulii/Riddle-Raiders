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
        
        bigPlayer.onPlayersDeath += NotifyPlayersDeath;
        bigPlayer.onPlayerHit += NotifyPlayerHit;
        smallPlayer.onPlayersDeath += NotifyPlayersDeath;
        smallPlayer.onPlayerHit += NotifyPlayerHit;
    }

    //TODO Add after passing/ not passing level
    public void DestroyPlayers()
    {
        bigPlayer.onPlayersDeath -= NotifyPlayersDeath;
        bigPlayer.onPlayerHit -= NotifyPlayerHit;
        smallPlayer.onPlayersDeath -= NotifyPlayersDeath;
        smallPlayer.onPlayerHit -= NotifyPlayerHit;
        
        Destroy(bigPlayer.gameObject);
        Destroy(smallPlayer.gameObject);

        bigPlayer = null;
        smallPlayer = null;
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

    private void NotifyPlayersDeath()
    {
        onPlayersDeath?.Invoke();
    }

}
