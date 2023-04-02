using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private PlatformController platformController;
    [SerializeField] private LeverController leverController;

    private void Start()
    {
        leverController.onLeverStateChange += HandleLeverStateChange;
    }

    private void OnDestroy()
    {
        leverController.onLeverStateChange -= HandleLeverStateChange;
    }

    private void HandleLeverStateChange(Lever.LeverState leverState, Platform.PlatformColor color)
    {
        platformController.MovePlatforms(leverState, color);
    }
}
