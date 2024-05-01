using System;
using UnityEngine;

public class HitDetector : MonoBehaviour
{
    public event Action onSpikeHit;
    public event Action onLavaEnter;
    public event Action onLavaExit;

    private void OnTriggerEnter2D(Collider2D col)
    {
        Spikes spikes = col.GetComponent<Spikes>();
        Lava lava = col.GetComponent<Lava>();

        if (spikes)
        {
            onSpikeHit?.Invoke();
        }

        if (lava)
        {
            onLavaEnter?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        Lava lava = col.GetComponent<Lava>();

        if (lava)
        {
            onLavaExit?.Invoke();
        }
    }
}
