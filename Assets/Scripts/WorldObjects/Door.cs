using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    //Assign onto the door itself.
    [SerializeField] private PhysicalButton doorButton;
    [SerializeField] private Vector3 openOffset;
    [SerializeField] private float doorSpeed;
    private Vector3 closedPosition;
    private bool isOpen = false;
    // Start is called before the first frame update
    void Start()
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
        isOpen = true;
    }

    public void CloseDoor()
    {
        isOpen = false;
    }
}