using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform door; // Assign this to your door object in the inspector
    public bool isOpened = false;
    public float rotationSpeed = 90f; // Degrees per second
    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        closedRotation = door.rotation;
        openRotation = Quaternion.Euler(door.eulerAngles + new Vector3(0, 90, 0)); // Rotate 90 degrees on Y axis
    }

    void Update()
    {
        // Smoothly rotate the door towards the target rotation
        Quaternion targetRotation = isOpened ? openRotation : closedRotation;
        door.rotation = Quaternion.RotateTowards(door.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            isOpened = !isOpened;
        }
    }
}
