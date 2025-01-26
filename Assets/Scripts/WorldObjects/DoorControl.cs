using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    //assign to the door trigger.
    [SerializeField] private bool lockDoorAfterExit;

    public GameObject door;
    public Door[] doors;
    public float timer;
    private void OnTriggerEnter(Collider other)
    {
        //Change door color (visual effects)
    }

    private void OnTriggerStay(Collider other)
    {
        //Detect every frame I'm in front of the door


        //BY INPUT LOGIC
        if (Input.GetKeyDown(KeyCode.F))
        {
            door.SetActive(false);
        }

        /* BY TIME LOGIC HERE
        timer += Time.deltaTime;

        if(timer >= 3)
        {
            
        }
        */
    }

    private void OnTriggerExit(Collider other)
    {
        if (lockDoorAfterExit)
        {
            foreach (Door door in doors)
            { 
                door.CloseDoor();
            }
            //door.SetActive(true);
        }

        timer = 0;

    }
}
