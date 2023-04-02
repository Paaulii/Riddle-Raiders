using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Lever : MonoBehaviour
{
    public Action<LeverState, Platform.PlatformColor> onChangeState;
    [SerializeField] private HingeJoint2D hingeJoint2D;
    [SerializeField] private Platform.PlatformColor platformColor;
    private LeverState state = LeverState.None;
    private float pullThreshold = 10.0f;
    
    void Update()
    {
        CheckLeverState();
    }

    private void CheckLeverState()
    {
        if (state != LeverState.Off && Math.Abs(hingeJoint2D.limits.max - hingeJoint2D.jointAngle) <= pullThreshold)
        {
            ChangeState(LeverState.Off);
        }
        
        if (state != LeverState.On && Math.Abs(hingeJoint2D.limits.min - hingeJoint2D.jointAngle) <= pullThreshold)
        {
            ChangeState(LeverState.On);
        }
    }
    
    private void ChangeState(LeverState state) {
        this.state = state;
        onChangeState?.Invoke(state, platformColor);
    }
    
    public enum LeverState
    {
        None,
        Off,
        On
    }
}
