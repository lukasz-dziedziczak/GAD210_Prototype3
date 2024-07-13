using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interaction : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] float reachLength;
    [SerializeField] LayerMask interactableLayers;
    [SerializeField] float holdingDistance;

    public Puzzle1Piece Puzzle1Piece { get; private set; }
    public Puzzle1PieceSlot Puzzle1PieceSlot { get; private set; }
    public Puzzle2Piece Puzzle2Piece { get; private set; }

    public bool HoldingPuzzlePiece { get; private set; }    
    public bool CanPickUp => Puzzle1Piece != null;
    public bool CanPlay => Puzzle1Piece != null || Puzzle1PieceSlot != null;

    private void Awake()
    {
        if (player == null) player = GetComponent<Player>();

        player.Input.OnLeftPress += OnLeftPress;
        player.Input.OnLeftRelease += OnLeftRelease;
        player.Input.OnRightPress += OnRightPress;
    }

    private void OnDisable()
    {
        player.Input.OnLeftPress -= OnLeftPress;
        player.Input.OnLeftRelease -= OnLeftRelease;
        player.Input.OnRightPress -= OnRightPress;
    }

    private void Update()
    {
        if (HoldingPuzzlePiece)
        {
            Puzzle1Piece.transform.position = holdingPosition;
        }

        else
        {
            Ray ray = player.Camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(ray, out RaycastHit hit, reachLength, interactableLayers))
            {
                if (hit.collider.TryGetComponent<Puzzle1Piece>(out Puzzle1Piece piece1))
                {
                    Puzzle1Piece = piece1;
                }

                else if (hit.collider.TryGetComponent<Puzzle1PieceSlot>(out Puzzle1PieceSlot slot))
                {
                    Puzzle1PieceSlot = slot;
                }

                else if (hit.collider.TryGetComponent<Puzzle2Piece>(out Puzzle2Piece piece2))
                {
                    Puzzle2Piece = piece2;
                }
            }
            else
            {
                ClearInteractables();
            }
        }
    }

    public void ClearInteractables()
    {
        Puzzle1Piece = null;
        Puzzle1PieceSlot = null;
        HoldingPuzzlePiece = false;
        Puzzle2Piece = null;
    }


    private void OnLeftPress()
    {
        if (Puzzle1Piece != null)
        {
            HoldingPuzzlePiece = true;
            Puzzle1Piece.PickUp();
        }
        else if (Puzzle2Piece != null)
        {
            Puzzle2Piece.Interaction();
        }
        else
        {
            Debug.LogWarning("Nothing to pickup");
        }

    }

    private void OnLeftRelease()
    {
        if (HoldingPuzzlePiece)
        {
            HoldingPuzzlePiece = false;
            Puzzle1Piece.Drop();
        }
    }

    private void OnRightPress()
    {
        if (Puzzle1Piece != null && Puzzle1Piece.InSlot)
        {
            //Debug.Log("Trying to play sound on piece");
            Puzzle1Piece.PlaySound();
        }

        else if (Puzzle1PieceSlot != null && Puzzle1PieceSlot.SlotOccupied)
        {
            //Debug.Log("Trying to play sound on slot");
            Puzzle1PieceSlot.PuzzlePieceInSlot.PlaySound();
        }

        else
        {
            Debug.Log("Unable to play sound");
        }
    }

    private Vector3 holdingPosition
    {
        get
        {
            return player.Camera.transform.position + (player.Camera.transform.forward * holdingDistance);
        }
    }
}
