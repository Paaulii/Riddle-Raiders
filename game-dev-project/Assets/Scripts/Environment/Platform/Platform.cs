using DG.Tweening;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public PlatformColor Color => platformColor;
    [SerializeField] private Transform shiftTransform;
    [SerializeField] private PlatformColor platformColor;
    [SerializeField] private float shiftDuration;

    private Vector3 originalPosition;
    private Vector3 shiftPosition;
    
    private void Start() {
        originalPosition = transform.position;
        shiftPosition = shiftTransform.position;
    }
    
    public void Shift()
    {
        transform.DOMove(shiftPosition, shiftDuration);
    }

    public void Return()
    {
        transform.DOMove(originalPosition, shiftDuration);
    }
    
    public enum PlatformColor {
        A,
        B,
        C,
        D,
        E,
        F
    }
}
