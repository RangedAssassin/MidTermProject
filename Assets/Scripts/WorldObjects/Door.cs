using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    //Assign onto the door itself.
    [SerializeField] private PhysicalButton doorButton;
    [SerializeField] private Vector3 openOffset;
    [SerializeField] private float doorSpeed;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Light lightSource;
    private Vector3 closedPosition;
    private bool isOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        closedPosition = transform.position;
        if (lightSource != null)
        {
            lightSource.enabled = false;
        }
        if (doorButton != null) doorButton.OnPressed.AddListener(OpenDoor);
        audioSource.loop = false;
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
        if (!audioSource.isPlaying)
        {
            SoundManager.SingleTon.PlaySound(audioSource);
        }
        if (lightSource != null)
        { 
            lightSource.enabled = true;
        }
    }

    public void CloseDoor()
    {
        isOpen = false;
        if (!audioSource.isPlaying)
        {
            SoundManager.SingleTon.PlaySound(audioSource);
        }
        if (lightSource != null)
        {
            lightSource.enabled = false;
        }
    }
}