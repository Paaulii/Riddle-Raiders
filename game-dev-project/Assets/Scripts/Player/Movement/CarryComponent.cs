
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarryComponent : MovementComponent
{
    public event Action onCatch; 
    public Box BoxInRange
    {
        get => boxInRange;
        set
        {
            if (value != null)
            {
                isNextToBox = true;
            }

            boxInRange = value;
        }
    }

    private float rotateSpeed;
    private float carryBoxHeight;
    private float carryMirrorHeight;
    private bool canCarry;
    private bool isNextToBox;
    private bool isCatchingBox;
    private bool isCatchingMirror;
    private Box caughtBox;
    private Box boxInRange;
    private Mirror caughtMirror;
    private InputAction scrollAction;
    private InputAction catchAction;
    
    public CarryComponent(Rigidbody2D rb, InputAction catchAction,
        InputAction scrollAction, float rotateSpeed, bool canCarry, float carryBoxHeight, float carryMirrorHeight) : base(rb)
    {
        this.scrollAction = scrollAction;
        this.catchAction = catchAction;
        this.rotateSpeed = rotateSpeed;
        this.carryBoxHeight = carryBoxHeight;
        this.carryMirrorHeight = carryMirrorHeight;
        this.canCarry = canCarry;
        
        this.catchAction.performed += HandleCatch;
        this.scrollAction.performed += RotateMirror;
    }

    ~CarryComponent()
    {
        catchAction.performed -= HandleCatch;
        scrollAction.performed -= RotateMirror;
    }
        
    public void TryCarryBox(Transform transform)
    {
        if (!isCatchingBox || caughtBox == null)
        {
            return;
        }
        
        float directionMultiplier = Math.Abs(transform.eulerAngles.y - 180f) < 0.001f ? 1 : -1;
        float distanceFromGround = isCatchingMirror ? carryMirrorHeight : carryBoxHeight;
        caughtBox.ChangePosition(transform.position + new Vector3(distanceFromGround * directionMultiplier, 0, 0));
    }

    private void RotateMirror(InputAction.CallbackContext obj)
    {
        if (!isCatchingMirror)
        {
            return;
        }
        
        float scrollValue = scrollAction.ReadValue<float>();
        float directionMultiplier = scrollValue > 0  ? 1 : -1;
        caughtMirror.Rotate(rotateSpeed * directionMultiplier);
    }

    private void HandleCatch(InputAction.CallbackContext context)
    {
        if (!canCarry)
        {
            return;
        }
        
        if (isNextToBox && !isCatchingBox && BoxInRange)
        {
            caughtBox = BoxInRange;
                
            Mirror mirror = caughtBox as Mirror;
              
            if (mirror)
            {
                caughtMirror = mirror;
                isCatchingMirror = true;
            }
                
            onCatch?.Invoke();
            isCatchingBox = true;
        }
        else if (isCatchingBox)
        {
            onCatch?.Invoke();
            isCatchingMirror = false;
            isCatchingBox = false;
            caughtBox = null;
            caughtMirror = null;
        }
    }
}
