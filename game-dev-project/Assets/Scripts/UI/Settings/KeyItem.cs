using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyItem : MonoBehaviour {
    [SerializeField] private InputActionReference action;
    [SerializeField] private bool isComposite;
    [SerializeField] private string name;

    [Header("UI")]
    [SerializeField] private UnityEngine.UI.Button bindButton;
    [SerializeField] private TextMeshProUGUI bindingDisplayNameText;

    private void Start()
    {
        bindButton.onClick.AddListener(RebindKey);
    }

    private void OnDestroy()
    {
        bindButton.onClick.RemoveListener(RebindKey);
    }

    public void RefreshContent()
    {
        bindingDisplayNameText.text = KeyBindingsManager.instance.GetKeyBindingText(action, isComposite, name);
    }
    
    private void RebindKey()
    {
        bindButton.gameObject.SetActive(false);
        KeyBindingsManager.instance.PerformKeyRebinding(action, isComposite, name);
        KeyBindingsManager.instance.onKeyRebinded += OnKeyRebinded;
    }

    private void OnKeyRebinded(InputActionReference action)
    {
        KeyBindingsManager.instance.onKeyRebinded -= OnKeyRebinded;
        bindButton.gameObject.SetActive(true);
        RefreshContent();
    }
}