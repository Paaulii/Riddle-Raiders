using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 100f;
    public float jumpForce = 6f;
    public float groundedLeeway = 0.1f;

    public KeyCode jumpKey = KeyCode.W;
    public KeyCode leftMove = KeyCode.A;
    public KeyCode rightMove = KeyCode.D;

    private Rigidbody2D rb = null;
    private Animator animator = null;

    private bool attemptJump = false;
    private bool attemptLeftMove = false;
    private bool attemptRightMove = false;
    private float moveIntentionX = 0;

    private void Awake()
    {
        if (GetComponent<Rigidbody2D>())
            rb = GetComponent<Rigidbody2D>();

        if (GetComponent<Animator>())
            animator = GetComponent<Animator>();
    }



    private bool CheckGrounded()
    {
        // Get the PolygonCollider2D component attached to the player object
        PolygonCollider2D playerCollider = GetComponent<PolygonCollider2D>();

        // Calculate the position of the ground check points
        Vector2 bottomLeft = new Vector2(playerCollider.bounds.min.x, playerCollider.bounds.min.y);
        Vector2 bottomRight = new Vector2(playerCollider.bounds.max.x, playerCollider.bounds.min.y);

        // Check if the player collider overlaps with any colliders on the "Ground" layer
        return Physics2D.OverlapArea(bottomLeft, bottomRight, LayerMask.GetMask("Ground"));
    }

    void Update()
    {
        GetInput();
        HandleJump();
        RunAnimation();
        HandleRun();
    }

    private void GetInput()
    {
        attemptLeftMove = Input.GetKeyDown(leftMove);
        attemptRightMove = Input.GetKeyDown(rightMove);
        attemptJump = Input.GetKeyDown(jumpKey);
    }

    private void HandleRun()
    {
        rb.velocity = new Vector2(moveIntentionX * speed, rb.velocity.y);

        if (attemptLeftMove)
            moveIntentionX = -1;

        else if (attemptRightMove)
            moveIntentionX = 1;

        if(Input.GetKeyUp(leftMove) || Input.GetKeyUp(rightMove))
            moveIntentionX = 0;


        if (moveIntentionX > 0 && transform.rotation.y == 0)
            transform.rotation = Quaternion.Euler(0, 180f, 0);

        else if (moveIntentionX < 0 && transform.rotation.y != 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);

    }

    private void HandleJump() //TODO
    {
       // Debug.Log(CheckGrounded());
        if (attemptJump && CheckGrounded())
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void RunAnimation()
    {
        if (moveIntentionX != 0)
            animator.SetBool("isMoving", true);
        else
            animator.SetBool("isMoving", false);
    }
}
