using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMusicManager : MonoBehaviour
{
    public enum Music
    {
        GameMusic1
    }
    [SerializeField] private AudioClip gameMusic1;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.GameMusic += PlayMusic;
        }

    }

    private void OnDisable()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.GameMusic -= PlayMusic;
        }

    }

    public void PlayMusic(Music music)
    {
        switch (music)
        {
            case Music.GameMusic1:
                audioSource.clip = gameMusic1;
                audioSource.loop = true;
                audioSource.Play();
                break;

            default:
                break;
        }
    }
}
