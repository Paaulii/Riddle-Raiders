using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Players")]
    [SerializeField] private GameObject bigPlayerObject;
    [SerializeField] private GameObject smallPlayerObject;

    [Header("UI objects")]
    public TextMeshProUGUI endText;

    private int smallPlayerHealth;
    private int bigPlayerHealth;

    private void Start()
    {
        smallPlayerHealth = smallPlayerObject.GetComponent<HealthManager>().health;
        bigPlayerHealth = bigPlayerObject.GetComponent<HealthManager>().health;
        endText.gameObject.SetActive(false);
    }

    public void PlayerDied()
    {
       // if (bigPlayerHealth == 0 || smallPlayerHealth == 0)
        endText.text = "YOU LOST";
        endText.gameObject.SetActive(true);
        StartCoroutine(LoadLevel());
    }

    private IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
