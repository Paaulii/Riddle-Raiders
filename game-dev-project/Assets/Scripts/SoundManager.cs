using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum Sounds
    {
        Jump, EndLevel, Collect, Hit, Wind, Box, Slide, Climb
    }
    [SerializeField] private AudioClip jump, endLevel, collect, hit, wind, box ,slide, climb;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
