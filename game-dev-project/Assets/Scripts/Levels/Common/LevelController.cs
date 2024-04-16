using UnityEngine;

public class LevelController : MonoBehaviour
{
    public Vector2 BigPlayerPosition => bigPlayerPos.position;
    public Vector2 SmallPlayerPosition => smallPlayerPos.position;
    
    [SerializeField] private Transform bigPlayerPos;
    [SerializeField] private Transform smallPlayerPos;
}
