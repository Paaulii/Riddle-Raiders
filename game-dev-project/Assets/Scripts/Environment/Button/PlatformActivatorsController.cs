using System;
using UnityEngine;

public class PlatformActivatorsController : MonoBehaviour {
    public Action<PlatformActivator.State, Platform.PlatformColor> onStateChange;
    private PlatformActivator[] platformActivators;

    private void Start() {
        GetActivatorsReference();
    }

    private void OnDestroy()
    {
        foreach (var activator in platformActivators)
        {
            activator.onChangeState -= NotifyActivatorStateChange;
        }
    }

    private void GetActivatorsReference()
    {
        platformActivators = GetComponentsInChildren<PlatformActivator>(true);
        
        foreach (var activator in platformActivators)
        {
            activator.onChangeState += NotifyActivatorStateChange;
        }
    }

    private void NotifyActivatorStateChange(PlatformActivator.State state, 
        Platform.PlatformColor platformGroup)
    {
        onStateChange?.Invoke(state, platformGroup);
    }
}
