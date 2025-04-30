using UnityEngine;

public class DoorController : MonoBehaviour
{
    Transform door;
    public bool isOpened = false;
    public float rotationSpeed = 90f; // Degrees per second
    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        door = this.transform;
        closedRotation = door.rotation;
        openRotation = Quaternion.Euler(door.eulerAngles + new Vector3(0, 90, 0)); // Rotate 90 degrees on Y axis
    }

    void Update()
    {
        Quaternion targetRotation = isOpened ? openRotation : closedRotation;
        door.rotation = Quaternion.RotateTowards(door.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    
}
