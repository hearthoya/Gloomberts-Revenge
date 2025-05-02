using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float rotationSpeed = 90f;
    public bool isOpened = false;
    public Transform hingePoint; // Assign this in the Inspector
    BoxCollider door;

    private float currentYRotation = 0f;

    private void Start()
    {
        door = GetComponent<BoxCollider>();
    }
    void Update()
    {
        float targetAngle = isOpened ? 90f : 0f;
        float step = rotationSpeed * Time.deltaTime;
        float newYRotation = Mathf.MoveTowards(currentYRotation, targetAngle, step);
        float deltaRotation = newYRotation - currentYRotation;

        if (isOpened)
        {
            door.isTrigger = true;
        }
        else
        {
            door.isTrigger = false;
        }
        // Rotate around the hinge point on the Y axis
        if (hingePoint != null)
        {
            transform.RotateAround(hingePoint.position, Vector3.up, deltaRotation);
        }

        currentYRotation = newYRotation;
    }
}
