using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyBindingsManager : Singleton<KeyBindingsManager>
{
    public event Action<InputActionReference> onKeyRebinded;
    [SerializeField] private PlayerInput playerInput;
    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;
    private GameSettingsSO gameSettings;
    
    private void Start()
    {
        gameSettings = GameManager.Instance.GameSettings;
    }

    public void ResetKeyBindings()
    {
        PlayerPrefs.DeleteKey(gameSettings.player1KeyBindingsConfKey);
        PlayerPrefs.DeleteKey(gameSettings.player2KeyBindingsConfKey);
    }
    
    public void SaveKeyBindings()
    {
        SaveKeyBindings(gameSettings.player1KeyBindingsConfKey, gameSettings.player1ActionMap);
        SaveKeyBindings(gameSettings.player2KeyBindingsConfKey, gameSettings.player2ActionMap);
    }
    
    public void LoadKeyBindings()
    {
        LoadKeyBindings(gameSettings.player1KeyBindingsConfKey, gameSettings.player1ActionMap);
        LoadKeyBindings(gameSettings.player2KeyBindingsConfKey, gameSettings.player2ActionMap);
    }
        
    public void PerformKeyRebinding(InputActionReference action, bool isComposite, string keyName)
    {
        if (isComposite && keyName != String.Empty) {
            var bindingIndex = action.action.bindings.IndexOf(x => x.isPartOfComposite && x.name == keyName);
            rebindingOperation = action.action.PerformInteractiveRebinding().WithTargetBinding(bindingIndex);
        }
        else
        {
            rebindingOperation = action.action.PerformInteractiveRebinding();
        }
        
        rebindingOperation
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindComplete(action))
            .Start();
    }

    public string GetKeyBindingText(InputActionReference action,  bool isComposite, string keyName)
    {
        int bindingIndex = GetActionBindingIndex(action, isComposite, keyName);
        return InputControlPath.ToHumanReadableString(action.action.bindings[bindingIndex].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
    }
    
    private void SaveKeyBindings(string confKey, string actionMap)
    {
        string rebinds = playerInput.actions.FindActionMap(actionMap).SaveBindingOverridesAsJson();
        PlayerPrefs.SetString(confKey, rebinds);
    }

    private void LoadKeyBindings(string confKey, string actionMap)
    {
        string rebinds = PlayerPrefs.GetString(confKey, string.Empty);
        playerInput.actions.FindActionMap(actionMap).LoadBindingOverridesFromJson(rebinds);
    }


    private int GetActionBindingIndex(InputActionReference action, bool isComposite, string keyName)
    {
        int bindingIndex;
        if (isComposite && keyName != String.Empty) {
            bindingIndex = action.action.bindings.IndexOf(x => x.isPartOfComposite && x.name == keyName);
        }
        else
        {
            bindingIndex = action.action.GetBindingIndexForControl(action.action.controls[0]);
        }

        return bindingIndex;
    }

    private void RebindComplete(InputActionReference action)
    {
        rebindingOperation.Dispose();
        onKeyRebinded?.Invoke(action);
    }
}
