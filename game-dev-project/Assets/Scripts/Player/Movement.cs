using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Movement : MonoBehaviour
{
    public Action onJump;
    public Action onClimb;
    public Action onCatch;
    public Action onSlide;
    public PlayerInput PlayerInput => playerInput;
    
    [Header("Movement")] 
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float speed = 100f;
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private float slideForce = 1f;
    [SerializeField] private float groundDistance = 0.1f;
    [SerializeField] private bool canCarry = false;

    [SerializeField] private Collider2D collider;
    
    private Rigidbody2D rb = null;
    private Animator animator = null;

    private float horizontal;
    private float vertical;
    private float beginGravity;

    private bool isNearbyLadder = false;
    private bool isClimbing = false;
    private bool isSliding = false;

    private bool isNextToBox = false;
    private bool isCatchingBox = false;
    private Box boxObject = null;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction catchAction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        beginGravity = rb.gravityScale;
        moveAction = playerInput.actions["Movement"];
        jumpAction = playerInput.actions["Jump"];
        catchAction = playerInput.actions["Catch"];

        jumpAction.performed += HandleJump;
        catchAction.performed += HandleCatch;
    }


    private void OnDestroy()
    {
        jumpAction.performed -= HandleJump;
        catchAction.performed -= HandleCatch;
    }

    private void Update()
    {
        GetInput();
        MoveOnLadder();
        RunAnimation();
        HandleRun();
        CarryBox();
    }
    
    public void ForceStopPlayer()
    {
        rb.velocity = Vector2.zero;
        enabled = false;
        animator.SetBool("isMoving", false);
    }

    private void HandleJump(InputAction.CallbackContext context)
    {
        if (CheckGrounded())
        {
            onJump?.Invoke();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void HandleCatch(InputAction.CallbackContext context)
    {
        if (canCarry)
        {
            if (isNextToBox && !isCatchingBox)
            {
                onCatch?.Invoke();
                isCatchingBox = true;
            }
            else if (isCatchingBox)
            {
                onCatch?.Invoke();
                isCatchingBox = false;
            }
        }
    }

    private void CarryBox()
    {
        if (isCatchingBox && boxObject != null)
        {
            if (transform.eulerAngles.y == 180f)
            {
                boxObject.transform.position = transform.position + new Vector3(1f, 0f, 0f);
            }
            else
            {
                boxObject.transform.position = transform.position + new Vector3(-1f, 0f, 0f);
            }
        }

    }

    private bool CheckGrounded()
    {
        Bounds bounds = collider.bounds;
        Vector2 bottomCenter = new Vector2(bounds.center.x, bounds.min.y);
        return Physics2D.Raycast(bottomCenter, -Vector2.up, groundDistance);
    }
    
    private void MoveOnLadder()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
        }
        else
        {
            rb.gravityScale = beginGravity;
        }
    }


    private void GetInput() 
    {
        if (isNearbyLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }
    }

    private void HandleRun()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        horizontal = input.x;
        vertical = input.y;
       // TO SMOOTH VELOCITY: currentInputVector = Vector2.SmoothDamp(currentInputVector, input, ref smoothInputVelocity, smoothInputSpeed);
       if (isSliding)
        {
            rb.AddForce(new Vector2(horizontal * slideForce, 0f));
        }
        else
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }

        if (horizontal > 0 && transform.rotation.y == 0)
            transform.rotation = Quaternion.Euler(0, 180f, 0);

        else if (horizontal < 0 && transform.rotation.y != 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);

    }

    private void RunAnimation()
    {
        if (horizontal != 0)
            animator.SetBool("isMoving", true);
        else
            animator.SetBool("isMoving", false);
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        boxObject = col.GetComponent<Box>();
        if (boxObject)
        {
            isNextToBox = true;
        }

        Ice ice = col.GetComponent<Ice>();
        if (ice)
        {
            onSlide?.Invoke();
            isSliding = true;
        }

        Ladder ladder = col.GetComponent<Ladder>();
        if (ladder)
        {
            onClimb?.Invoke();
            isNearbyLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        boxObject = other.GetComponent<Box>();
        if (boxObject)
        {
            isNextToBox = false;
        }

        Ice ice = other.GetComponent<Ice>();
        if (ice)
        {
            isSliding = false;
        }
        Ladder ladder = other.GetComponent<Ladder>();
        if (ladder)
        {
            isNearbyLadder = false;
            isClimbing = false;
        }
    }
}
