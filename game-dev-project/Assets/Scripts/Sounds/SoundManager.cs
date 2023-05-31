using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private List<Movement> movements = new List<Movement>();
    [SerializeField] private GameManager gameManager;
    public enum Sounds
    {
        Jump, EndLevel, Collect, Hit, Wind, Box, Slide, Climb
    }
    [SerializeField] private AudioClip jump, endLevel, collect, hit, wind, box, slide, climb;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Geyser geyser = FindObjectOfType<Geyser>();
        if (geyser != null)
        {
            geyser.GeyserSound += PlaySound;
        }

        foreach (Movement movement in movements)
        {
            if (movement != null)
            {
                movement.JumpSound += PlaySound;
                movement.BoxSound += PlaySound;
                movement.SlideSound += PlaySound;
            }
        }

        if (gameManager != null)
        {
            gameManager.CollectSound += PlaySound;
            gameManager.HitSound += PlaySound;
            gameManager.EndSound += PlaySound;
        }

    }

    private void OnDisable()
    {
        Geyser geyser = FindObjectOfType<Geyser>();
        if (geyser != null)
        {
            geyser.GeyserSound -= PlaySound;
        }

        foreach (Movement movement in movements)
        {
            if (movement != null)
            {
                movement.JumpSound -= PlaySound;
                movement.BoxSound -= PlaySound;
                movement.SlideSound -= PlaySound;
            }
        }

        if (gameManager != null)
        {
            gameManager.CollectSound -= PlaySound;
            gameManager.HitSound -= PlaySound;
            gameManager.EndSound -= PlaySound;
        }

    }

    public void PlaySound(Sounds sound)
    {
        switch (sound)
        {
            case Sounds.Jump:
                audioSource.PlayOneShot(jump);
                break;

            case Sounds.EndLevel:
                audioSource.PlayOneShot(endLevel);
                break;

            case Sounds.Collect:
                audioSource.PlayOneShot(collect);
                break;

            case Sounds.Hit:
                audioSource.PlayOneShot(hit);
                break;

            case Sounds.Wind:
                audioSource.PlayOneShot(wind);
                break;

            case Sounds.Box:
                audioSource.PlayOneShot(box);
                break;

            case Sounds.Slide:
                audioSource.PlayOneShot(slide);
                break;

            case Sounds.Climb:
                audioSource.PlayOneShot(climb);
                break;

            default:
                break;
        }
    }
}
