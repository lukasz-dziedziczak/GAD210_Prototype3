using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interaction : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] float reachLength;
    [SerializeField] LayerMask interactableLayers;
    [SerializeField] float holdingDistance;

    PuzzlePiece puzzlePiece;
    PuzzlePieceSlot puzzlePieceSlot;

    bool holdingPuzzlePiece;
    public bool CanPickUp => puzzlePiece != null;
    public bool CanPlay => puzzlePiece != null || puzzlePieceSlot != null;

    private void Awake()
    {
        if (player == null) player = GetComponent<Player>();

        player.Input.OnPickupPress += OnPickupPress;
        player.Input.OnPickupRelease += OnPickupRelease;
        player.Input.OnPlaySoundPress += OnPlaySoundPress;
    }

    private void OnDisable()
    {
        player.Input.OnPickupPress -= OnPickupPress;
        player.Input.OnPickupRelease -= OnPickupRelease;
        player.Input.OnPlaySoundPress -= OnPlaySoundPress;
    }

    private void Update()
    {
        if (holdingPuzzlePiece)
        {
            puzzlePiece.transform.position = holdingPosition;
        }

        else
        {
            Ray ray = player.Camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(ray, out RaycastHit hit, reachLength, interactableLayers))
            {
                if (hit.collider.TryGetComponent<PuzzlePiece>(out PuzzlePiece piece))
                {
                    puzzlePiece = piece;
                }

                else if (hit.collider.TryGetComponent<PuzzlePieceSlot>(out PuzzlePieceSlot slot))
                {
                    puzzlePieceSlot = slot;
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
        puzzlePiece = null;
        puzzlePieceSlot = null;
        holdingPuzzlePiece = false;
    }


    private void OnPickupPress()
    {
        if (puzzlePiece != null)
        {
            holdingPuzzlePiece = true;
            puzzlePiece.PickUp();
        }
        else
        {
            Debug.LogWarning("Nothing to pickup");
        }

    }

    private void OnPickupRelease()
    {
        if (holdingPuzzlePiece)
        {
            holdingPuzzlePiece = false;
            puzzlePiece.Drop();
        }
    }

    private void OnPlaySoundPress()
    {
        if (puzzlePiece != null && puzzlePiece.InSlot)
        {
            //Debug.Log("Trying to play sound on piece");
            puzzlePiece.PlaySound();
        }

        else if (puzzlePieceSlot != null && puzzlePieceSlot.SlotOccupied)
        {
            //Debug.Log("Trying to play sound on slot");
            puzzlePieceSlot.PuzzlePieceInSlot.PlaySound();
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
