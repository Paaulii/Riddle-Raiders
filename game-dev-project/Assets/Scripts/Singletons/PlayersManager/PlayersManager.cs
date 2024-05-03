using System;
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
        InitPlayer(bigPlayerPrefab, levelController.BigPlayerPosition, levelController.transform, ref bigPlayer);
        InitPlayer(smallPlayerPrefab, levelController.SmallPlayerPosition, levelController.transform, ref smallPlayer);
    }

    public void ForceStopPlayersMovement()
    {
        smallPlayer.ForceStopMovement();
        bigPlayer.ForceStopMovement();
    }
    
    public void ForceStartPlayersMovement()
    {
        bigPlayer.ForceStartMovement();
        smallPlayer.ForceStartMovement();
    }
        
    public void DestroyPlayers()
    {
        DestroyCharacter(bigPlayer);
        DestroyCharacter(smallPlayer);
    }

    private void DestroyCharacter(Character character)
    {
        if (character != null)
        { 
            character.onPlayersDeath -= HandlePlayerDeath;
            character.onPlayerHit -= HandlePlayerHit;
            Destroy(character.gameObject);
        }
    }
    
    private void InitPlayer(Character prefab, Vector2 spawnPos, Transform parent, ref Character spawnedCharacter)
    {
        spawnedCharacter = Instantiate(prefab, spawnPos, Quaternion.identity, parent);
        spawnedCharacter.onPlayersDeath += HandlePlayerDeath;
        spawnedCharacter.onPlayerHit += HandlePlayerHit;
        spawnedCharacter.onSpikeHit += HandlePlayerSpikeHit;
        spawnedCharacter.onCatch += HandlePlayerCatchObject;
        spawnedCharacter.onJump += HandlePlayerJump;
        spawnedCharacter.onSlide += HandlePlayerSlide;
        spawnedCharacter.onStartClimb += HandlePlayerStartClimb;
        spawnedCharacter.onStopClimb += HandlePlayerStopClimb;
    }

    private void HandlePlayerStopClimb(Character character)
    {
        SoundManager.Instance.SetLoop(false);
    }

    private void HandlePlayerStartClimb(Character character)
    {
        SoundManager.Instance.SetLoop(true);
        SoundManager.Instance.PlaySound(SoundManager.GameSoundType.Climb, character.transform.position);
    }

    private void HandlePlayerSlide(Character character)
    {
        SoundManager.Instance.PlaySound(SoundManager.GameSoundType.Slide, character.transform.position);
    }

    private void HandlePlayerJump(Character character)
    {
        SoundManager.Instance.PlaySound(SoundManager.GameSoundType.Jump, character.transform.position);
    }

    private void HandlePlayerCatchObject(Character character)
    {
        SoundManager.Instance.PlaySound(SoundManager.GameSoundType.Box, character.transform.position);
    }

    private void HandlePlayerSpikeHit(Character character)
    {
        ParticleEffectsSystem.instance.SpawnEffect(EffectType.Blood, character.transform.position);
    }

    private void HandlePlayerHit(Character character)
    {
        SoundManager.Instance.PlaySound(SoundManager.GameSoundType.Hit, character.transform.position); 
        character.GetComponent<Animator>().SetTrigger("attack");
        onPlayerHit?.Invoke(character);
    }

    private void HandlePlayerDeath(Character character)
    {
        ParticleEffectsSystem.instance.SpawnEffect(EffectType.Skull, character.transform.position);
        onPlayersDeath?.Invoke();
        Destroy(character.gameObject);
    }
}
