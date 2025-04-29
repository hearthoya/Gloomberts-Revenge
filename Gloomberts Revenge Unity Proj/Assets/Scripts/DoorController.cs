using UnityEngine;

public class DoorController : MonoBehaviour
{
    Transform door;
    bool isOpened = false;
    bool playerInTrigger = false;
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
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            isOpened = !isOpened;
        }

        Quaternion targetRotation = isOpened ? openRotation : closedRotation;
        door.rotation = Quaternion.RotateTowards(door.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
        }
    }
}
