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

        CheckForPlayer();
    }

    void CheckForPlayer()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayer);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
            {
                isOpened = !isOpened;
                break;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
