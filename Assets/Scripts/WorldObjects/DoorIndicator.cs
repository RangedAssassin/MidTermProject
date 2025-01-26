using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorIndicator : MonoBehaviour
{
    [SerializeField] private bool lockDoorAfterExit;
    [SerializeField] private bool needsKey;

    public Door leftDoor;
    public Door rightDoor;

    public float timer;
    [SerializeField] private Material shade;
    [SerializeField] private Light shadeLight;

    private void Awake()
    {


    }

    private void OnEnable()
    {
        DoorKeyCard.KeycardPickedup += DoorKeyCardKeycardPickedup;
    }

    private void DoorKeyCardKeycardPickedup()
    {
        GameObject targetObject = GameObject.Find("Shade");
        GameObject targetObject2 = GameObject.Find("Light");
        if (targetObject != null)
        {
            shade = targetObject.GetComponent<Renderer>().material;
            if (shade != null)
            {
                shade.color = Color.green;
            }
            else
            {
                Debug.LogWarning("Material not found on the target");
            }

        }
        if (targetObject2 != null)
        {
            shadeLight = targetObject2.GetComponent<Light>();
            if (shadeLight != null)
            {
                shadeLight.color = Color.green;
            }
            else
            {
                Debug.LogWarning("No Light to change");
            }
        }
        //shade.SetColor(name, Color.green);
        //shadeLight.color = Color.green;
    }

    private void OnDisable()
    {
        DoorKeyCard.KeycardPickedup -= DoorKeyCardKeycardPickedup;
    }
    private void OnTriggerEnter(Collider other)
    {
        //change door colour (Visual Effects)
        Debug.Log("Trigger Entered");
        leftDoor.OpenDoor();
        rightDoor.OpenDoor();
    }

    private void OnTriggerStay(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {
        if (lockDoorAfterExit == true)
        {
            StartCoroutine("LockAfterClosing");
        }
        else
        {
            leftDoor.CloseDoor();
            rightDoor.CloseDoor();
        }
    }

    IEnumerator LockAfterClosing()
    {
        leftDoor.CloseDoor();
        rightDoor.CloseDoor();
        yield return new WaitForSeconds(2);
        Destroy(leftDoor);
        Destroy(rightDoor);
    }
}


//-----------------------------------------------------------------------------------------------------------------------



public class DoorTest : MonoBehaviour
{
    [SerializeField] private PhysicalButton doorButton;
    [SerializeField] private Vector3 openOffset;
    [SerializeField] private float doorSpeed;

    private Vector3 closedPosition;

    private bool isOpen = false;
    private bool isLocked = true;


    private void OnEnable()
    {
        DoorKeyCard.KeycardPickedup += DoorKeyCardKeycardPickedup;
    }

    private void DoorKeyCardKeycardPickedup()
    {
        isLocked = false;

    }

    private void OnDisable()
    {
        DoorKeyCard.KeycardPickedup -= DoorKeyCardKeycardPickedup;
    }
    private void Start()
    {
        closedPosition = transform.position;

        if (doorButton != null) doorButton.OnPressed.AddListener(OpenDoor);
    }

    private void Update()
    {
        if (isOpen)
        {
            Vector3 targetPosition = closedPosition + openOffset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * doorSpeed);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, closedPosition, Time.deltaTime * doorSpeed);
        }
    }
    public void OpenDoor()
    {
        if (isLocked == true)
        {
            Debug.Log("Sorry, the door is locked");
        }
        else
        {
            isOpen = true;
        }
    }

    public void CloseDoor()
    {

        isOpen = false;
    }
}


