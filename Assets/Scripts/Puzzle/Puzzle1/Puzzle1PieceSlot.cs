using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Puzzle1PieceSlot : MonoBehaviour
{
    [SerializeField] Vector3 puzzlePiecePosition;
    [SerializeField] Puzzle1 puzzle;

    public Vector3 PuzzlePiecePosition => puzzlePiecePosition;
    public Puzzle1Piece PuzzlePieceInSlot;
    public Puzzle1Piece RemovingPiece;
    public bool SlotOccupied => PuzzlePieceInSlot != null;
    public Puzzle1 Puzzle => puzzle;
    public AudioSource AudioSource
    {
        get
        {
            if (PuzzlePieceInSlot != null) return PuzzlePieceInSlot.AudioSource;
            return null;
        }
    }


    private void Awake()
    {
        if (puzzle == null) puzzle = GetComponentInParent<Puzzle1>();

        Player.Instance.Input.OnLeftRelease += Input_OnLeftRelease;
    }

    private void Input_OnLeftRelease()
    {
        if (RemovingPiece != null) RemovingPiece = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        print(name + " has " + other.name + " in slot area");
        if (PuzzlePieceInSlot != null) return;

        if (other.TryGetComponent<Puzzle1Piece>(out Puzzle1Piece puzzlePiece))
        {
            if (puzzlePiece == RemovingPiece) return;

            puzzlePiece.InsertIntoSlot(this);
            PuzzlePieceInSlot = puzzlePiece;
            Player.Instance.Interaction.ClearInteractables();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Puzzle1Piece>(out Puzzle1Piece puzzlePiece) &&
            puzzlePiece == RemovingPiece)
        {
            //RemovingPiece = null;
        }
    }

    public void RemoveFromSlot()
    {
        if (PuzzlePieceInSlot == null) return;

        RemovingPiece = PuzzlePieceInSlot;
        PuzzlePieceInSlot = null;
        RemovingPiece.transform.parent = null;

        Debug.Log("Removed " + RemovingPiece.name + " from " + name);
    }
}
