using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPuzzle : Puzzle
{
    //private IPuzzlePiece[] allPuzzlePieces;

    //private void Awake()
    //{
    //    allPuzzlePieces = GetComponentsInChildren<IPuzzlePiece>();

    //    foreach (IPuzzlePiece piece in allPuzzlePieces) 
    //    {
    //        piece.LinkToPuzzle(this);
    //    }
    //}

    private void Update()
    {
        if (CheckSoultion() && isPuzzleComplete == false)
        {
            OnPuzzleCompleted?.Invoke();
            isPuzzleComplete = true;
        }
    }

    public override bool CheckSoultion()
    {
        foreach (IPuzzlePiece piece in allPuzzlePieces)
        {
            if (!piece.IsCorrect())
            {
                return false;
            }
        }
        return true;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        isPuzzleActive = true;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        isPuzzleActive = false;
    //    }
    //}
}
