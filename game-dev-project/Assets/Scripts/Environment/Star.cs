using System;
using UnityEngine;

public class Star : MonoBehaviour 
{
    public Action<Star> onStarCollect;
    [SerializeField] private GameObject pickupStartEffect;
    private void OnTriggerEnter2D(Collider2D col)
    {
        Character character = col.GetComponent<Character>();

        if (!character)
        {
            return;
        }
        
        onStarCollect?.Invoke(this);
        Destroy(gameObject);
    }
}
