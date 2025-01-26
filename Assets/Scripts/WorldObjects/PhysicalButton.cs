using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicalButton : MonoBehaviour, IInteractable
{
    public UnityEvent OnPressed;
    public void StartInteraction()
    {
        Debug.Log("Pressed Button");
        OnPressed?.Invoke();
    }

    public void StopInteraction()
    {

    }
}
