using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpComponent : MovementComponent
{
    public event Action onJump;

    private float jumpForce;
    private float groundDistance;
    
    private Collider2D collider;
    private InputAction jumpAction;

    public JumpComponent(Rigidbody2D rb, Collider2D col, InputAction inputAction, float jumpForce, float groundDistance) : base(rb)
    {
        collider = col;
        jumpAction = inputAction;
        this.jumpForce = jumpForce;
        this.groundDistance = groundDistance;
        jumpAction.performed += Jump;
    }
    
    ~JumpComponent()
    {
        jumpAction.performed -= Jump;
    }

    private void Jump(InputAction.CallbackContext callbackContext)
    {
        if (CheckGrounded())
        {
            onJump?.Invoke();
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
        }
    }
    
    private bool CheckGrounded()
    {
        Bounds bounds = collider.bounds;
        Vector2 bottomCenter = new Vector2(bounds.center.x, bounds.min.y);
        Vector2 bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        Vector2 bottomRight = new Vector2(bounds.max.x, bounds.min.y);

        RaycastHit2D hitCenter = Physics2D.Raycast(bottomCenter, -Vector2.up, groundDistance);
        RaycastHit2D hitLeft = Physics2D.Raycast(bottomLeft, -Vector2.up, groundDistance);
        RaycastHit2D hitRight = Physics2D.Raycast(bottomRight, -Vector2.up, groundDistance);

        return hitLeft.collider != null || hitCenter.collider != null || hitRight.collider != null;
    }
}
