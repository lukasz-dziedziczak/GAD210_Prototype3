using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzlePieceSlot : MonoBehaviour
{
    [SerializeField] Vector3 puzzlePiecePosition;
    [SerializeField] Puzzle puzzle;

    public Vector3 PuzzlePiecePosition => puzzlePiecePosition;
    public PuzzlePiece PuzzlePieceInSlot;
    public PuzzlePiece RemovingPiece;
    public bool SlotOccupied => PuzzlePieceInSlot != null;
    public Puzzle Puzzle => puzzle;
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
        if (puzzle == null) puzzle = GetComponentInParent<Puzzle>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PuzzlePieceInSlot != null) return;

        if (other.TryGetComponent<PuzzlePiece>(out PuzzlePiece puzzlePiece))
        {
            if (puzzlePiece == RemovingPiece) return;

            puzzlePiece.InsertIntoSlot(this);
            PuzzlePieceInSlot = puzzlePiece;
            Player.Instance.Interaction.ClearInteractables();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PuzzlePiece>(out PuzzlePiece puzzlePiece) &&
            puzzlePiece == RemovingPiece)
        {
            RemovingPiece = null;
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
