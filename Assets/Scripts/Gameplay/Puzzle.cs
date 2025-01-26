using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class Puzzle : MonoBehaviour
{
    public UnityEvent OnPuzzleCompleted;
    public bool isPuzzleActive;
    public bool isPuzzleComplete;
    public abstract bool CheckSoultion();
}
