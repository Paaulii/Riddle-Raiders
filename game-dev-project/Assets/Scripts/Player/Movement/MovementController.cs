using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    public Action onJump;
    public Action onStartClimb;
    public Action onStopClimb;
    public Action onCatch;
    public Action onSlide;
    public bool IsMoving => isMoving;
    
    [SerializeField] private Collider2D collider;
    [SerializeField] private Rigidbody2D rb;

    [Header("Movement")] 
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float speed = 100f;
    [SerializeField] private float slideForce = 1f;
    
    [Header("Jump properties")]
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private float groundDistance = 0.1f;
    
    [Header("Carry properties")]
    [SerializeField] private bool canCarry = false;
    [SerializeField] private float carryBoxHeight;
    [SerializeField] private float carryMirrorHeight;

    [Header("Rotate ability")] 
    [SerializeField] private float rotateSpeed;
    
    private float horizontal;
    private float vertical;
    private float beginGravity;
    
    private bool isSliding;
    private bool isMoving;
    
    private InputAction moveAction;
    private bool isOnPlatform;

    private JumpComponent jumpComponent;
    private ClimbComponent climbComponent;
    private CarryComponent carryComponent;

    private void Awake()
    {
        moveAction = playerInput.actions["Movement"];
        SetupComponents();
    }

    private void SetupComponents()
    {
        jumpComponent = new JumpComponent(rb, collider, playerInput.actions["Jump"], jumpForce, groundDistance);
        jumpComponent.onJump += NotifyJumpExecuted;
        
        carryComponent = new CarryComponent(rb, playerInput.actions["Catch"], playerInput.actions["Rotate"], 
            rotateSpeed, canCarry, carryBoxHeight, carryMirrorHeight);
        carryComponent.onCatch += NotifyOnCatch;
        
        climbComponent = new ClimbComponent(rb, speed, rb.gravityScale);
    }

    private void NotifyOnCatch()
    {
        onCatch?.Invoke();
    }

    private void NotifyJumpExecuted()
    {
        onJump?.Invoke();
    }

    private void Update()
    {
        climbComponent.TryMoveOnLadder(vertical);
        carryComponent.TryCarryBox(transform);
        HandleRun();
    }
    
    private void OnDestroy()
    {
        jumpComponent.onJump -= NotifyJumpExecuted;
        carryComponent.onCatch -= NotifyOnCatch;
    }

    public void ForceStopPlayer()
    {
        rb.velocity = Vector2.zero;
        enabled = false;
        isMoving = false;
    }

    public void ForceStartPlayer()
    {
        enabled = true;
        isMoving = true;
    }
    
    private void HandleRun()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        horizontal = input.x;
        vertical = input.y; 
        if (isSliding) 
        {
            rb.AddForce(new Vector2(horizontal * slideForce, 0f));
        }
        else
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }

        if (horizontal > 0 && transform.rotation.y == 0)
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
        else if (horizontal < 0 && transform.rotation.y != 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        
        isMoving = horizontal != 0;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        carryComponent.BoxInRange = col.GetComponent<Box>();
        
        Ice ice = col.GetComponent<Ice>();
        if (ice)
        {
            onSlide?.Invoke();
            isSliding = true;
        }

        Ladder ladder = col.GetComponent<Ladder>();
        if (ladder)
        {
            onStartClimb?.Invoke();
            climbComponent.IsNerbyLadder = true;
        }

        TryAttachToPlatform(col);
    }

    private void TryAttachToPlatform(Collider2D col)
    {
        if (!isOnPlatform) 
        {
            Platform platform = col.GetComponent<Platform>();

            if (platform)
            {
                isOnPlatform = true;
                gameObject.transform.SetParent(platform.transform);
            }
        }
        else
        {
            gameObject.transform.SetParent(null);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        carryComponent.BoxInRange = other.GetComponent<Box>();

        Ice ice = other.GetComponent<Ice>();
        if (ice)
        {
            isSliding = false;
        }
        Ladder ladder = other.GetComponent<Ladder>();
        
        if (ladder)
        {
            onStopClimb?.Invoke();
            climbComponent.IsNerbyLadder = false;
        }
        
        DetachFromPlatform(other);
    }

    private void DetachFromPlatform(Collider2D other)
    {
        Platform platform = other.GetComponent<Platform>();

        if (platform)
        {
            isOnPlatform = false;
            gameObject.transform.SetParent(null);
        }
    }
}
