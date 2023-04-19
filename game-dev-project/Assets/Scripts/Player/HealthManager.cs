using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HealthManager : MonoBehaviour
{
    [Header("Health value")]
    public int health=3;

    [Header("UI objects")]
    [SerializeField] private Image heart1;
    [SerializeField] private Image heart2;
    [SerializeField] private Image heart3;

    private void Update()
    {
        switch (health)
        {
            case 2:
                heart1.enabled = false;
                break;
            case 1:
                heart2.enabled = false;
                break;
            case 0:
                heart3.enabled = false;
                break;
            default:
                break;
        }           
    }
}
