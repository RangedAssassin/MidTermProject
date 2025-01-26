using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAbility : MonoBehaviour
{
    [SerializeField] private GravityAbility myGravity;
    [SerializeField] private float jumpForce;
    public void Jump()
    {
        if (myGravity.IsOnGround())
        {
            myGravity.AddForce(Vector3.up * jumpForce);
        }
    }


}
