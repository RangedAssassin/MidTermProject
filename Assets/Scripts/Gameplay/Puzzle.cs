using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class Puzzle : MonoBehaviour
{
    protected IPuzzlePiece[] allPuzzlePieces;

    private void Awake()
    {
        allPuzzlePieces = GetComponentsInChildren<IPuzzlePiece>();
    }
    
    public UnityEvent OnPuzzleCompleted;
    
    public bool isPuzzleActive;
    
    public bool isPuzzleComplete;
    
    public abstract bool CheckSoultion();
}
