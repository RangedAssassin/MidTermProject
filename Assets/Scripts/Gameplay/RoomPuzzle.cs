using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPuzzle : Puzzle
{

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
}
