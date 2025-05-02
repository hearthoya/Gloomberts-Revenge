using UnityEngine;
using UnityEngine.UI;

public class FirstPersonController : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Audio")]
    public AudioSource walkingAudioSource;
    public AudioClip walkingClip;

    public AudioSource sprintingAudioSource;
    public AudioClip sprintingClip;

    public AudioSource jumpAudioSource;
    public AudioClip jumpClip;

    [Tooltip("Camera Settings")]
    public Camera playerCamera;

    public float fov = 60f;
    public bool invertCamera = false;
    public bool cameraCanMove = true;
    public float mouseSensitivity = 2f;
    public float maxLookAngle = 50f;

    // Crosshair
    public bool lockCursor = true;
    public bool crosshair = true;
    public Sprite crosshairImage;
    public Color crosshairColor = Color.white;

    [Tooltip("Internal Variables")]
    // Internal Variables
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private Image crosshairObject;



    // Movement Variables
    [Tooltip("Movement Variables")]
    public bool playerCanMove = true;
    public float walkSpeed = 5f;
    public float maxVelocityChange = 10f;

    // Internal Variables
    private bool isWalking = false;

    // Sprint

    public bool enableSprint = true;
    public bool unlimitedSprint = false;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public float sprintSpeed = 7f;
    public float sprintDuration = 5f;
    public float sprintCooldown = .5f;
    public float sprintFOV = 80f;
    public float sprintFOVStepTime = 10f;

    // Sprint Bar
    public bool useSprintBar = true;
    public bool hideBarWhenFull = true;
    public Image sprintBarBG;
    public Image sprintBar;
    public float sprintBarWidthPercent = .3f;
    public float sprintBarHeightPercent = .015f;

    // Internal Variables
    private CanvasGroup sprintBarCG;
    private bool isSprinting = false;
    private float sprintRemaining;
    private float sprintBarWidth;
    private float sprintBarHeight;
    private bool isSprintCooldown = false;
    private float sprintCooldownReset;

    // Jump
    [Tooltip("Jump Settings")]
    public bool enableJump = true;
    public KeyCode jumpKey = KeyCode.Space;
    public float jumpPower = 5f;
    public LayerMask groundMask;

    // Internal Variables
    private bool isGrounded = false;

    // Crouch
    [Tooltip("Crouch Settings")]
    public bool enableCrouch = true;
    public bool holdToCrouch = true;
    public KeyCode crouchKey = KeyCode.LeftControl;
    public float crouchHeight = .75f;
    public float speedReduction = .5f;

    // Internal Variables
    private bool isCrouched = false;
    private Vector3 originalScale;

    // Head Bob
    [Tooltip("Head Bob Settings")]
    public bool enableHeadBob = true;
    public Transform joint;
    public float bobSpeed = 10f;
    public Vector3 bobAmount = new Vector3(.15f, .05f, 0f);

    // Internal Variables
    private Vector3 jointOriginalPos;

    // Key Variables
    [Tooltip("Key Settings")]
    public KeyCode interactKey = KeyCode.E;
    public float pickupRange = 3f;

    [Tooltip("Pause Stuff")]
    public KeyCode pauseKey = KeyCode.Escape;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        crosshairObject = GetComponentInChildren<Image>();

        // Set internal variables
        playerCamera.fieldOfView = fov;
        originalScale = transform.localScale;
        jointOriginalPos = joint.localPosition;

        if (!unlimitedSprint)
        {
            sprintRemaining = sprintDuration;
            sprintCooldownReset = sprintCooldown;
        }
    }

    void Start()
    {
        // Crosshair
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (crosshair)
        {
            crosshairObject.sprite = crosshairImage;
            crosshairObject.color = crosshairColor;
        }
        else
        {
            crosshairObject.gameObject.SetActive(false);
        }

        // Sprint Bar

        sprintBarCG = GetComponentInChildren<CanvasGroup>();

        if (useSprintBar)
        {
            sprintBarBG.gameObject.SetActive(true);
            sprintBar.gameObject.SetActive(true);

            float screenWidth = Screen.width;
            float screenHeight = Screen.height;

            sprintBarWidth = screenWidth * sprintBarWidthPercent;
            sprintBarHeight = screenHeight * sprintBarHeightPercent;

            sprintBarBG.rectTransform.sizeDelta = new Vector3(sprintBarWidth, sprintBarHeight, 0f);
            sprintBar.rectTransform.sizeDelta = new Vector3(sprintBarWidth - 2, sprintBarHeight - 2, 0f);

            if (hideBarWhenFull)
            {
                sprintBarCG.alpha = 0;
            }
        }
    }

    float camRotation;

    private void Update()
    {
        if (PauseMenu.isPaused)
        {
            cameraCanMove = false;
        }
        else
        {
            cameraCanMove = true;
        }
        // Control camera movement
        if (cameraCanMove)
        {
            yaw = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;

            if (!invertCamera)
            {
                pitch -= mouseSensitivity * Input.GetAxis("Mouse Y");
            }
            else
            {
                // Inverted Y
                pitch += mouseSensitivity * Input.GetAxis("Mouse Y");
            }

            // Clamp pitch between lookAngle
            pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);

            transform.localEulerAngles = new Vector3(0, yaw, 0);
            playerCamera.transform.localEulerAngles = new Vector3(pitch, 0, 0);
        }

        // Sprint

        if (enableSprint)
        {
            if (isSprinting)
            {
                playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, sprintFOV, sprintFOVStepTime * Time.deltaTime);

                // Drain sprint remaining while sprinting
                if (!unlimitedSprint)
                {
                    sprintRemaining -= 1 * Time.deltaTime;
                    if (sprintRemaining <= 0)
                    {
                        isSprinting = false;
                        isSprintCooldown = true;
                    }
                }
            }
            else
            {
                // Regain sprint while not sprinting
                sprintRemaining = Mathf.Clamp(sprintRemaining += 1 * Time.deltaTime, 0, sprintDuration);
            }

            // Handles sprint cooldown 
            // When sprint remaining == 0 stops sprint ability until hitting cooldown
            if (isSprintCooldown)
            {
                sprintCooldown -= 1 * Time.deltaTime;
                if (sprintCooldown <= 0)
                {
                    isSprintCooldown = false;
                }
            }
            else
            {
                sprintCooldown = sprintCooldownReset;
            }

            // Handles sprintBar 
            if (useSprintBar && !unlimitedSprint)
            {
                float sprintRemainingPercent = sprintRemaining / sprintDuration;
                sprintBar.transform.localScale = new Vector3(sprintRemainingPercent, 1f, 1f);
            }
        }

        // Jump

        // Gets input and calls jump method
        if (enableJump && Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }

        

        // Crouch

        if (enableCrouch)
        {
            if (Input.GetKeyDown(crouchKey) && !holdToCrouch)
            {
                Crouch();
            }

            if (Input.GetKeyDown(crouchKey) && holdToCrouch)
            {
                isCrouched = false;
                Crouch();
            }
            else if (Input.GetKeyUp(crouchKey) && holdToCrouch)
            {
                isCrouched = true;
                Crouch();
            }
        }

        CheckGround();

        if (enableHeadBob)
        {
            HeadBob();
        }
        // Key Pickup Logic
        if (Input.GetKeyUp(interactKey))
        {
            GameObject item = CheckItems();
            if (item != null) {
                MapManager.ItemCheck(item);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key pressed");
            if (PauseMenu.isPaused)
            {
                PauseMenu.Resume();
            }
            else
            {
                PauseMenu.Pause();
            }

        }

    }

    void FixedUpdate()
    {
        // Movement

        if (playerCanMove)
        {
            // Calculate how fast we should be moving
            Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            // Checks if player is walking and isGrounded
            // Will allow head bob
            if (targetVelocity.x != 0 || targetVelocity.z != 0 && isGrounded)
            {
                isWalking = true;
            }
            else
            {
                isWalking = false;
            }

            // All movement calculations shile sprint is active
            if (enableSprint && Input.GetKey(sprintKey) && sprintRemaining > 0f && !isSprintCooldown)
            {
                targetVelocity = transform.TransformDirection(targetVelocity) * sprintSpeed;

                // Apply a force that attempts to reach our target velocity
                Vector3 velocity = rb.linearVelocity;
                Vector3 velocityChange = (targetVelocity - velocity);
                velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
                velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
                velocityChange.y = 0;

                // Player is only moving when valocity change != 0
                // Makes sure fov change only happens during movement
                if (velocityChange.x != 0 || velocityChange.z != 0)
                {
                    isSprinting = true;

                    if (sprintingAudioSource && sprintingClip && !sprintingAudioSource.isPlaying)
                    {
                        sprintingAudioSource.clip = sprintingClip;
                        sprintingAudioSource.loop = true;
                        sprintingAudioSource.Play();
                    }


                    if (isCrouched)
                    {
                        Crouch();
                    }

                    if (hideBarWhenFull && !unlimitedSprint)
                    {
                        sprintBarCG.alpha += 5 * Time.deltaTime;
                    }
                }

                rb.AddForce(velocityChange, ForceMode.VelocityChange);
            }
            // All movement calculations while walking
            else
            {
                isSprinting = false;

                // Stop sprinting audio if it's playing
                if (sprintingAudioSource && sprintingAudioSource.isPlaying)
                {
                    sprintingAudioSource.Stop();
                }

                // PLAY walking audio
                if ((targetVelocity.x != 0 || targetVelocity.z != 0) && isGrounded)
                {
                    if (walkingAudioSource && walkingClip && !walkingAudioSource.isPlaying)
                    {
                        walkingAudioSource.clip = walkingClip;
                        walkingAudioSource.loop = true;
                        walkingAudioSource.Play();
                    }
                }
                else
                {
                    if (walkingAudioSource && walkingAudioSource.isPlaying)
                    {
                        walkingAudioSource.Stop();
                    }
                }

                targetVelocity = transform.TransformDirection(targetVelocity) * walkSpeed;

                // Apply a force that attempts to reach our target velocity
                Vector3 velocity = rb.linearVelocity;
                Vector3 velocityChange = (targetVelocity - velocity);
                velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
                velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
                velocityChange.y = 0;

                rb.AddForce(velocityChange, ForceMode.VelocityChange);
            }
        }
    }

    // Sets isGrounded based on a raycast sent straigth down from the player object
    private void CheckGround()
    {
        Vector3 origin = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y * .5f), transform.position.z);
        Vector3 direction = transform.TransformDirection(Vector3.down);
        float distance = 1f;
        bool hit = Physics.Raycast(origin, direction, distance, groundMask);
        if (hit)
        {
            // Debug.Log("We are did it");
            Debug.DrawRay(origin, direction * distance, Color.red);
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(0f, jumpPower, 0f, ForceMode.Impulse);
            isGrounded = false;

            if (jumpAudioSource && jumpClip)
                jumpAudioSource.PlayOneShot(jumpClip);
        }

        if (isCrouched && !holdToCrouch)
        {
            Crouch();
        }
    }

    public GameObject CheckItems () {

        // Get the player's position (origin of the spherecast)
        Vector3 origin = transform.position;
        float sphereRadius = 0.9f;

    // Perform the sphere overlap (without direction)
    Collider[] hitColliders = Physics.OverlapSphere(origin, sphereRadius);

        foreach (var collider in hitColliders)
        {
            if (collider.CompareTag("Key"))
            {
                Debug.Log("Found object with the tag: Key");
                // You can interact with the object here
                return collider.gameObject;
            }
            if (collider.CompareTag("Locked Door"))
            {
                Debug.Log("Found object with the tag: Locked Door");
                return collider.gameObject;
            }
            if (collider.CompareTag("Door"))
            {
                Debug.Log("Found object with the tag: Door");
                return collider.gameObject;
            }
            if (collider.CompareTag("Vent"))
            {
                Debug.Log("Found object with the tag: Vent");
                return collider.gameObject;
            }
            if (collider.CompareTag("Drill"))
            {
                Debug.Log("Found object with the tag: Drill");
                return collider.gameObject;
            }
        }
        return null;
    }
    



    private void Crouch()
    {
        // Stands player up to full height
        // Brings walkSpeed back up to original speed
        if (isCrouched)
        {
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
            walkSpeed /= speedReduction;

            isCrouched = false;
        }
        // Crouches player down to set height
        // Reduces walkSpeed
        else
        {
            transform.localScale = new Vector3(originalScale.x, crouchHeight, originalScale.z);
            walkSpeed *= speedReduction;

            isCrouched = true;
        }
    }

    private void HeadBob()
    {
        if (isWalking)
        {
            // Calculates HeadBob speed during sprint
            // Sprinting sound
            if (isSprinting && sprintingClip != null)
            {
                if (!sprintingAudioSource.isPlaying)
                {
                    sprintingAudioSource.clip = sprintingClip;
                    sprintingAudioSource.loop = true;
                    sprintingAudioSource.Play();
                }

                if (walkingAudioSource.isPlaying)
                {
                    walkingAudioSource.Stop();
                }
            }
            else
            {
                if (sprintingAudioSource.isPlaying)
                {
                    sprintingAudioSource.Stop();
                }
            }

            // Walking sound
            if (isWalking && !isSprinting && isGrounded && walkingClip != null)
            {
                if (!walkingAudioSource.isPlaying)
                {
                    walkingAudioSource.clip = walkingClip;
                    walkingAudioSource.loop = true;
                    walkingAudioSource.Play();
                }
            }
            else
            {
                if (walkingAudioSource.isPlaying)
                {
                    walkingAudioSource.Stop();
                }
            }
        }
    }
}
 