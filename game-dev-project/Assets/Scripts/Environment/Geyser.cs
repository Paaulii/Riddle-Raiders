using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Geyser : MonoBehaviour
{
    [Header("Jump settings")]
    [SerializeField] private float power;
    public event Action<SoundManager.Sounds> GeyserSound;

    private Rigidbody2D rb;
    private Vector2 position;

    private void AddForce(Collider2D collision)
    {
        if (collision.name == "Small Player" || collision.name == "Big Player")
        {
            position = new(0, power);
            rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce(position, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GeyserSound?.Invoke(SoundManager.Sounds.Wind);
        AddForce(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        AddForce(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        AddForce(collision);
    }
}
