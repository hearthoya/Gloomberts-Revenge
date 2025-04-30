using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float rotationSpeed = 90f;
    public float detectionRadius = 1.5f;
    public LayerMask detectionLayer;

    public bool isOpened = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, 90, 0));
    }

    void Update()
    {
        Quaternion targetRotation = isOpened ? openRotation : closedRotation;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }



}
