using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Movement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 100f;
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private float groundDistance = 0.1f;

    [SerializeField] private string horizontalAxis;
    [SerializeField] private KeyCode jumpKey = KeyCode.W;

    [SerializeField] private Collider2D collider;
    
    private Rigidbody2D rb = null;
    private Animator animator = null;
    private bool attemptJump = false;
    
    private float horizontal;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private bool CheckGrounded()
    {
        Bounds bounds = collider.bounds;
        Vector2 bottomCenter = new Vector2(bounds.center.x, bounds.min.y);
        return Physics2D.Raycast(bottomCenter, -Vector2.up, groundDistance);
    }

    private void Update()
    {
        GetInput();
        HandleJump();
        RunAnimation();
        HandleRun();
    }


    private void GetInput()
    {
        horizontal = Input.GetAxisRaw(horizontalAxis);
        attemptJump = Input.GetKeyDown(jumpKey);
    }

    private void HandleRun()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        
        if (horizontal > 0 && transform.rotation.y == 0)
            transform.rotation = Quaternion.Euler(0, 180f, 0);

        else if (horizontal < 0 && transform.rotation.y != 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);

    }

    private void HandleJump()
    {
        if (attemptJump && CheckGrounded())
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void RunAnimation()
    {
        if (horizontal != 0)
            animator.SetBool("isMoving", true);
        else
            animator.SetBool("isMoving", false);
    }
}
