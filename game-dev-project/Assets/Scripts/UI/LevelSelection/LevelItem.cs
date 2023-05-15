using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LevelItem : MonoBehaviour 
{
    public Action<LevelItem> onLevelClicked;
    public int LevelNumber => levelNumber;
    [SerializeField] private Image starsImage;
    [SerializeField] private TextMeshProUGUI levelNumberText;
    [SerializeField] private Sprite[] stars;
    [SerializeField] private Image lockedLevelCover;
    [SerializeField] private UnityEngine.UI.Button button;

    bool isLevelLocked = false;
    int levelNumber; 
    private void Awake()
    {
        button.onClick.AddListener(() => onLevelClicked?.Invoke(this));
    }

    public void SetData(int starsAmount, int levelNumber, bool isLocked)
    {
        levelNumberText.text = levelNumber.ToString();
        starsImage.sprite = stars[starsAmount];
        isLevelLocked = isLocked;
        this.levelNumber = levelNumber;
        
        if (isLevelLocked)
        {
            lockedLevelCover.gameObject.SetActive(true);
        }
    }
}
