using System;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public event Action onEnterDoor; 
    public Vector2 BigPlayerPosition => bigPlayerPos.position;
    public Vector2 SmallPlayerPosition => smallPlayerPos.position;
    
    [SerializeField] private Transform bigPlayerPos;
    [SerializeField] private Transform smallPlayerPos;
    [SerializeField] private EnterEndDoor endDoor;

    private void Awake()
    {
        endDoor.onEnterEndDoor += NotifyEnterDoor;
    }

    private void OnDestroy()
    {
        endDoor.onEnterEndDoor -= NotifyEnterDoor;
    }

    private void NotifyEnterDoor()
    {
        onEnterDoor?.Invoke();
    }
}
