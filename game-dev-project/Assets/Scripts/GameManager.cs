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
    void Update()
    {
        if(bigPlayerObject.GetComponent<HealthManager>().health == 0 || smallPlayerObject.GetComponent<HealthManager>().health == 0)
        {
            endText.text = "You Lost";
            endText.gameObject.SetActive(true);
            StartCoroutine(LoadLevel());
        }
    }
    private IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
