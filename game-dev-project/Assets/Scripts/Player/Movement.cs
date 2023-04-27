using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Movement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 100f;
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private float slideForce = 1f;
    [SerializeField] private float groundDistance = 0.1f;
    [SerializeField] private string horizontalAxis;
    [SerializeField] private string verticalAxis;
    [SerializeField] private KeyCode jumpKey = KeyCode.W;
    [SerializeField] private KeyCode catchKey = KeyCode.E;

    [SerializeField] private Collider2D collider;
    
    private Rigidbody2D rb = null;
    private Animator animator = null;
    private bool attemptJump = false;
    private bool attemptCatch = false;

    private float horizontal;
    private float vertical;
    private float beginGravity;

    private bool isNearbyLadder = false;
    private bool isClimbing = false;
    private bool isSliding = false;

    private bool isNextToBox = false;
    public bool isCatchingBox = false;
    private Box boxObject = null;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        beginGravity = rb.gravityScale;
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
        MoveOnLadder();
        HandleJump();
        RunAnimation();
        HandleRun();
        HandleCatch();
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
        horizontal = Input.GetAxisRaw(horizontalAxis);
        vertical = Input.GetAxis(verticalAxis);
        attemptJump = Input.GetKeyDown(jumpKey);
        attemptCatch = Input.GetKeyDown(catchKey);
        
        if (isNearbyLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }
    }

    private void HandleRun()
    {
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

    private void HandleCatch()
    {
        if (isCatchingBox)
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
        if (attemptCatch && isNextToBox && !isCatchingBox)
        {
            isCatchingBox = true;
        }
        else if (attemptCatch && isCatchingBox)
        {
            isCatchingBox = false;
        }
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
            isSliding = true;
        }

        Ladder ladder = col.GetComponent<Ladder>();
        if (ladder)
        {
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
