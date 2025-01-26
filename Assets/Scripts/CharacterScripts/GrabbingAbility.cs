using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbingAbility : MonoBehaviour
{
    [SerializeField] private Transform holdingHand;
    [SerializeField] private float syncStrenght;

    private Rigidbody objectInHold;
    public void PickupObject(Rigidbody toGrab)
    {
        if (objectInHold != null)
        {
            DropDownObject();
            return;
        }

        objectInHold = toGrab;
        toGrab.useGravity = false;
        toGrab.drag = 10;
        toGrab.transform.position = holdingHand.position;
        toGrab.constraints = RigidbodyConstraints.FreezeRotation;
        
        //toGrab.isKinematic = true;
        //toGrab.transform.SetParent(holdingHand, true);
    }

    public void DropDownObject()
    {
        if (objectInHold != null)
        {
            objectInHold.useGravity = true;
            objectInHold.drag = 0;
            
            objectInHold.constraints = RigidbodyConstraints.None;
            objectInHold = null;


            //objectInHold.isKinematic = false;
            //objectInHold.transform.SetParent(null);
        }
    }

    private void Update()
    {
        if (objectInHold != null && Vector3.Distance(holdingHand.position, objectInHold.transform.position) > 0.1f)
        { 
            MoveObject();
        }
    }
    public void MoveObject()
    {
        Vector3 targetdirection = holdingHand.position - objectInHold.transform.position;
        objectInHold.AddForce(targetdirection * syncStrenght);
    }
}
