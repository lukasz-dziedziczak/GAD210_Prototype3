using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle2 : Puzzle
{
    [SerializeField] Puzzle2Piece[] puzzlePieces;
    [SerializeField] Door door;

    int currentIndex = -1;

    public void ResetPuzzle()
    {
        currentIndex = 0;
        currentIndex++;
        PuzzlePieceByIndex(currentIndex).PlaySound();
    }

    public void ActivatePuzzlePiece(int index)
    {
        if (index == currentIndex)
        {
            PuzzlePieceByIndex(currentIndex).StopSound();
            currentIndex++;
            PuzzlePieceByIndex(currentIndex).PlaySound();
        }
    }

    public Puzzle2Piece PuzzlePieceByIndex(int index)
    {
        foreach(Puzzle2Piece piece in puzzlePieces)
        {
            if (piece.Index == index) return piece;
        }
        return null;
    }

    public override void BeginPuzzle()
    {
        ResetPuzzle();
    }
}
