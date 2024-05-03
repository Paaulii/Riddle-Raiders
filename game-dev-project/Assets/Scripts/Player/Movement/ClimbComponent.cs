using UnityEngine;

public class ClimbComponent : MovementComponent
{
    public bool IsNerbyLadder
    {
        get => isNearbyLadder;
        set
        {
            if (value == false)
            {
                isClimbing = false;
            }
            
            isNearbyLadder = value;
        }
    }

    private bool isNearbyLadder;
    private bool isClimbing;
    private readonly float climbingSpeed;
    private readonly float beginGravity;

    
    public void TryMoveOnLadder(float vertical)
    {
        CheckIfIsClimbing(vertical);
        
        if (isClimbing)
        {
            rigidbody.gravityScale = 0f;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, vertical * climbingSpeed);
        }
        else
        {
            rigidbody.gravityScale = beginGravity;
        }
    }
    
    private void CheckIfIsClimbing(float vertical) 
    {
        if (isNearbyLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }
    }
    
    public ClimbComponent(Rigidbody2D rb, float climbingSpeed, float beginGravity) : base(rb)
    {
        this.climbingSpeed = climbingSpeed;
        this.beginGravity = beginGravity;
    }
}
