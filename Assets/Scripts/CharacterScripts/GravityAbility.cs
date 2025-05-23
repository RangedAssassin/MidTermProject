using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GravityAbility : MonoBehaviour
{
    [SerializeField] private float isOnGroundCheckSphereSize = 0.01f;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private CharacterController controller;
    [SerializeField] private float gravityAcceleration = -9.81f;
    private float currentGravity;


    private void FixedUpdate()
    {
        if (!IsOnGround())
        {
            currentGravity += gravityAcceleration * Time.deltaTime;
            if (currentGravity < -15f)
            {
                currentGravity = -15f;
            }
        }
        else if (currentGravity < 0)
        {
            currentGravity = 0;
        }

        Vector3 gravityVector = new Vector3();
        gravityVector.y = currentGravity;
        controller.Move(gravityVector * Time.deltaTime);
    }

    public bool IsOnGround()
    {
        return Physics.CheckSphere(transform.position, isOnGroundCheckSphereSize, groundLayer);
    }
    public void AddForce(Vector3 force)
    {
        currentGravity = force.y;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, isOnGroundCheckSphereSize);
    }
}