using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKeyCard : MonoBehaviour
{
    public static event Action KeycardPickedup;

    public AudioSource pickupSound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pickupSound.Play();
            Debug.Log("KeyCard Picked-up!");
            KeycardPickedup?.Invoke();
            Destroy(gameObject,0.5f);
        }
    }
}
