
using UnityEngine;

public class MovementComponent
{
    protected Rigidbody2D rigidbody;

    public MovementComponent(Rigidbody2D rb)
    {
        rigidbody = rb;
    }
}
