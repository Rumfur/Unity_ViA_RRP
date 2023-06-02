using UnityEngine;

public class ScaleObject : MonoBehaviour
{
    [Tooltip("Define the speed of the scale animation")]
    public float scaleSpeed;
    [Tooltip("Define the maximum scaleUp")]
    public float maxScale;
    [Tooltip("Define the maximum scaleUp")]
    public float minScale;
    private Vector3 scaleUp;
    private Vector3 scaleDown;
    private float scaleInt = 1;

    void Start() {
        scaleUp = new Vector3(scaleSpeed, scaleSpeed, scaleSpeed);
        scaleDown = new Vector3(-scaleSpeed, -scaleSpeed, -scaleSpeed);
    }

    void Update()
    {
        scaleInt += scaleSpeed;
        if (scaleInt > maxScale || scaleInt <= minScale) {
            scaleSpeed *= -1;
        }
        if (scaleSpeed > 0) {
            transform.localScale += scaleUp;
        } else {
            transform.localScale += scaleDown;
        }
        
    }
}
