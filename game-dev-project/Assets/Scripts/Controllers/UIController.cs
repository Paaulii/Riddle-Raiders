using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("UI objects")]
    [SerializeField] private PlayerStateController smallPlayerStateController;
    [SerializeField] private PlayerStateController bigPlayerStateController;
    [SerializeField] private GameObject gameOverText;

    private void Start()
    {
        SetActiveGameOverText(false);
    }

    public void DecreasePlayersHealth(Character.CharacterType characterType)
    {
        switch (characterType)
        {
            case Character.CharacterType.Big:
                bigPlayerStateController.DecreaseHeartsAmount();
                break;
            case Character.CharacterType.Small:
                smallPlayerStateController.DecreaseHeartsAmount();
                break;
        }
    }

    public void SetActiveGameOverText(bool isActive)
    {
        gameOverText.SetActive(isActive);
    }
}
