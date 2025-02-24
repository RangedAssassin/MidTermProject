using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    //assign to the door trigger.
    [SerializeField] private bool lockDoorAfterExit;
    [SerializeField] private bool needsKey;

    public Door[] doors;

    [SerializeField] private Material shade;
    [SerializeField] private Light shadeLight;

    private void OnEnable()
    {
        DoorKeyCard.KeycardPickedup += DoorKeyCardKeycardPickedup;
    }

    private void DoorKeyCardKeycardPickedup()
    {
        needsKey = false;
        GameObject targetObject = GameObject.Find("Shade");
        GameObject targetObject2 = GameObject.Find("ShadeLight");
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
    }

    private void OnDisable()
    {
        DoorKeyCard.KeycardPickedup -= DoorKeyCardKeycardPickedup;
    }
    private void OnTriggerEnter(Collider other)
    {
        //change door colour (Visual Effects)
  
        if (needsKey == true)
        {
            Debug.Log("Needs a keycard to open!!");
        }
        else
        {
            foreach (Door door in doors)
            {
                door.OpenDoor();
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {

        if (lockDoorAfterExit == true && needsKey == false)
        {
            StartCoroutine("LockAfterClosing");
        }
        else
        {
            foreach (Door door in doors)
            {
                door.CloseDoor();
            }
        }
    }

    private IEnumerator LockAfterClosing()
    {
        foreach (Door door in doors)
        {
            door.CloseDoor();
        }
        yield return new WaitForSeconds(1);
        needsKey = true;
    }

}

