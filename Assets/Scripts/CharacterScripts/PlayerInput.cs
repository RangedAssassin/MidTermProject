using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{
    //Singleton pattern
    public static PlayerInput Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //singleton Pattern

    [Header("Script References")]
    [SerializeField] private MoveAbility moveAbilty;
    [SerializeField] private LookAbility lookAbilty;
    [SerializeField] private ShootingAbility shootAbility;
    [SerializeField] private JumpAbility jumpAbility;
    [SerializeField] private InteractAbility interactAbility;
    //[SerializeField] private CommanderAbility commandAbility;

    //Directional Inputs
    private Vector2 lookDirection;

    //reference to the head/camera GameObject
    [SerializeField] private CharacterController controller;
    private Vector3 moveDir;

    [SerializeField] private float mouseSensitivity;

    [SerializeField] private float checkSphereSize = 0.01f;
    [SerializeField] private float pushStrength = 10f; // Adjustable push force

    private Transform currentPlatform;
    private Vector3 lastPlatformPosition;


    private Vector3 platformVelocity;
    private Transform platform;

    private float timeToEnableInput = 2f;
    private float timer = 0;

    void Start()
    {
        //Lambda expression
        GetComponent<HealthSystem>().OnDead += () =>
        {
            this.enabled = false;
            EnableCursor();
        };
        //Lambda expression

        DisableCursor();
    }


    void Update()
    {
        if (timer < timeToEnableInput)
        {
            timer += timeToEnableInput;
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpAbility.Jump();
        }

        if (moveAbilty != null)
        {
            moveDir = new Vector3();
            moveDir.x = Input.GetAxis("Horizontal");
            moveDir.z = Input.GetAxis("Vertical");
            moveAbilty.Move(moveDir);

            // Prevent moving into a blocked box
            if (IsPushingBlocked(moveDir))
            {
                moveDir = Vector3.zero; // Stop movement
            }
        }
        // Apply platform movement
        if (currentPlatform != null)
        {
            Vector3 platformMovement = currentPlatform.position - lastPlatformPosition;
            controller.Move(platformMovement);
            lastPlatformPosition = currentPlatform.position;
        }

        if (lookAbilty)
        {
            lookDirection.x += Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
            lookDirection.y += Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

            float angleOnY = lookDirection.y;
            lookDirection.y = Mathf.Clamp(angleOnY, -80, 80);

            lookAbilty.Look(lookDirection);
        }

        if (shootAbility != null && Input.GetMouseButtonDown(0))
        {
            shootAbility.Shoot();
        }

        if (interactAbility && Input.GetKeyDown(KeyCode.F))
        {
            //Debug.Log("F button Pressed");
            interactAbility.Interact();
        }

        //if (commandAbility && Input.GetMouseButtonDown(1))
        //{
        //    commandAbility.Command();
        //}
    }

    //Testing the sphere location
    private void OnDrawGizmos()
    {
        //Drawing a sphere at the feet of the player
        Gizmos.DrawSphere(transform.position, checkSphereSize);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rb = hit.collider.attachedRigidbody;

        if (hit.collider.CompareTag("MovingPlatform"))
        {
            platform = hit.collider.transform;
            platformVelocity = platform.GetComponent<Rigidbody>() ? platform.GetComponent<Rigidbody>().velocity : Vector3.zero;
        }
        else
        {
            platform = null;
            platformVelocity = Vector3.zero;
        }

        // Ensure object has Rigidbody and isn't kinematic
        if (rb != null && !rb.isKinematic)
        {
            Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z); // Ignore Y-axis
            rb.velocity = pushDirection * pushStrength;
        }
    }
    public Vector3 GetMoveDirection()
    {
        return moveDir; // Adjust based on your input system
    }

    // Check if the player is trying to push into a blocked box
    private bool IsPushingBlocked(Vector3 moveDirection)
    {
        float checkDistance = 0.2f; // Adjust as needed
        RaycastHit hit;

        if (Physics.Raycast(transform.position, moveDirection, out hit, checkDistance))
        {
            return hit.collider != null && hit.collider.CompareTag("PushableBox");
        }

        return false;
    }

    public void EnableCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void DisableCursor()
    {
        //Controls of mouse cursor
        Cursor.visible = false; //Visibilty to hidden
        Cursor.lockState = CursorLockMode.Locked; //Locked To the Center of the screen
    }
}
