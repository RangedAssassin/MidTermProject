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
    [SerializeField] private CommanderAbility commandAbility;

    //Directional Inputs
    private Vector2 lookDirection;

    //reference to the head/camera GameObject
    [SerializeField] private CharacterController controller;

    [SerializeField] private float mouseSensitivity;

    [SerializeField] private float checkSphereSize = 0.01f;

    private Transform currentPlatform;

    private Vector3 lastPlatformPosition;


    void Start()
    {
        
        //Lambda expression
        GetComponent<HealthSystem>().OnDead += () =>
        {
            this.enabled = false;
        };
        //Lambda expression

        //Controls of mouse cursor
        Cursor.visible = false; //Visibilty to hidden
        Cursor.lockState = CursorLockMode.Locked; //Locked To the Center of the screen
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpAbility.Jump();
        }

        if (moveAbilty != null)
        {
            Vector3 moveDir = new Vector3();
            moveDir.x = Input.GetAxis("Horizontal");
            moveDir.z = Input.GetAxis("Vertical");
            moveAbilty.Move(moveDir);
        }
        // Apply platform movement
        if (currentPlatform != null)
        {

            Vector3 platformMovement = currentPlatform.position - lastPlatformPosition;
            controller.Move(platformMovement * Time.deltaTime);
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
            interactAbility.Interact();
        }

        if (commandAbility && Input.GetMouseButtonDown(1))
        {
            commandAbility.Command();
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (hit.collider.CompareTag("MovingPlatform"))
        {
            gameObject.transform.parent = hit.transform.parent;
        }
        else if (!hit.collider.CompareTag("MovingPlatform"))
        {
            gameObject.transform.parent = null;
        }
    }

    //Testing the sphere location
    private void OnDrawGizmos()
    {
        //Drawing a sphere at the feet of the player
        Gizmos.DrawSphere(transform.position, checkSphereSize);
    }

}
