using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public void ChangePosition(Vector2 position)
    {
        transform.position = position;
    }
}
