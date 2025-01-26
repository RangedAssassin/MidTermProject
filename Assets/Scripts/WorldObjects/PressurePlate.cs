using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour, IPuzzlePiece
{
    [SerializeField] private bool unlockWithAnyObjects;
    [SerializeField] private Rigidbody[] correctRigidbody;
    
    public UnityEvent OnPressureStart = new UnityEvent();
    public UnityEvent OnPressureExit = new UnityEvent();

    private bool isPressed;
    private void OnTriggerEnter(Collider other)
    {
        foreach (Rigidbody rb in correctRigidbody)
        {
            if (unlockWithAnyObjects || rb == other.attachedRigidbody)
            {
                OnPressureStart.Invoke();
                isPressed = true;
                return;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (Rigidbody rb in correctRigidbody)
        {
            if (unlockWithAnyObjects || rb == other.attachedRigidbody)
            {
                OnPressureExit.Invoke();
                isPressed = false;
                return;
            }
        }
    }

    public void LinkToPuzzle(Puzzle p)
    {

    }

    public bool IsCorrect()
    {
        return isPressed;
    }
}
