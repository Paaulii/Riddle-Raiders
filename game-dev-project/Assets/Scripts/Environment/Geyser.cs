using UnityEngine;

public class Geyser : MonoBehaviour
{
    [SerializeField] private float power;
    
    private void AddForce(Collider2D collision)
    {
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        if (rb == null) 
        {
            return;
        }

        Vector2 force = new Vector2(0, power-rb.velocity.y);
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        AddForce(collision);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.Instance.PlaySound(SoundManager.GameSoundType.Wind, collision.transform.position);
    }
}
