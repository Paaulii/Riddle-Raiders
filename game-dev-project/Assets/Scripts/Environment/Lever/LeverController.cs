using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    public Action<Lever.LeverState, Platform.PlatformColor> onLeverStateChange;
    private Lever[] levers;

    private void Start()
    {
        InitLevers();
    }

    private void InitLevers()
    {
        levers = GetComponentsInChildren<Lever>(true);
        
        foreach (var lever in levers)
        {
            lever.onChangeState += (state, color) =>
            {
                onLeverStateChange?.Invoke(state, color);
            };
        }
    }
}
