using UnityEngine;

public class MoveObjectTrigger : MonoBehaviour
{
    [Tooltip("Should the object increase speed?")]
    public bool cumulativeSpeed;
    [Tooltip("Define the object's moving speed X")]
    public float speedIncreaseX;
    [Tooltip("Define the object's moving speed Y")]
    public float speedIncreaseY;
    [Tooltip("Define the object's moving speed Z")]
    public float speedIncreaseZ;
    [Tooltip("Define the maximum distance")]
    public float distance;
    public static Vector3 moveDistance;
    public static float movingSpeedX = 0;
    public static float movingSpeedY = 0;
    public static float movingSpeedZ = 0;
    public bool moving = false;
    public bool triggered = false;

    void Update()
    {
        if (moving) {
            if (cumulativeSpeed) {
                speedIncreaseX += speedIncreaseX/100;
                speedIncreaseY += speedIncreaseY/100;
                speedIncreaseZ += speedIncreaseZ/100;
            }
            movingSpeedX += speedIncreaseX;
            movingSpeedY += speedIncreaseY;
            movingSpeedZ += speedIncreaseZ;
            moveDistance = new Vector3(movingSpeedX, movingSpeedY, movingSpeedZ);
            transform.position += moveDistance;
            if (movingSpeedX > distance || movingSpeedY > distance || movingSpeedZ > distance) {
                moving = false;
            }
        }
    }

    private void OnTriggerEnter(Collider coliderObject)
    {
        if (coliderObject.tag == "Player"){
            if(!triggered){
            moving = true;
            triggered = true;
            }
        }
    }
}