using System;
using UnityEngine;

public class Lever : PlatformActivator
{
    [SerializeField] private HingeJoint2D hingeJoint2D;
    private float pullThreshold = 10.0f;
    
    void Update()
    {
        CheckLeverState();
    }

    private void CheckLeverState()
    {
        if (state != State.Off && Math.Abs(hingeJoint2D.limits.max - hingeJoint2D.jointAngle) <= pullThreshold)
        {
            ChangeState(State.Off);
        }
        
        if (state != State.On && Math.Abs(hingeJoint2D.limits.min - hingeJoint2D.jointAngle) <= pullThreshold)
        {
            ChangeState(State.On);
        }
    }
}
