using UnityEngine;

public class Mirror : Box
{
    [SerializeField] GameObject movingPart;

    public void Rotate(float rotateValue)
    {
        Vector3 currentRotation = movingPart.transform.localEulerAngles;
        movingPart.transform.localEulerAngles =  new Vector3(currentRotation.x, currentRotation.y, currentRotation.z + rotateValue);
    }
}
