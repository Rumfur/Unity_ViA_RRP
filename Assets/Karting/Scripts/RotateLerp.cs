using UnityEngine;

public class RotateLerp : MonoBehaviour
{
    public Vector3 startRotation;
    public Vector3 endRotation;
    private float rotationLevel = 0;
    private Vector3 currentRotation;

    void Update()
    {
        currentRotation = Vector3.Lerp(startRotation, endRotation, rotationLevel);
        transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, currentRotation.z);
    }

    public void setRotationLevel(float rotationLvl) {
        rotationLevel = rotationLvl;
    }
}
