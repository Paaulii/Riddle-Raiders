using UnityEngine;

public class PlayerKeys : MonoBehaviour
{
    [SerializeField] private KeyItem[] keys;

    public void RefreshKeys()
    {
        foreach (var key in keys)
        {
            key.RefreshContent();   
        }
    }
}
