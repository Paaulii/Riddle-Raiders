using System;
using UnityEngine;

public class PlatformActivator : MonoBehaviour 
{
    public event Action<State, Platform.PlatformColor> onChangeState;
    
    [SerializeField] protected Platform.PlatformColor platformColor;
    
    protected State state = State.None;

    protected void ChangeState(State state)
    {
        if (state == this.state)
        {
            return;
        }
        
        this.state = state;
        onChangeState?.Invoke(state, platformColor);
    }

    public enum State
    {
        None,
        Off,
        On
    }
}
