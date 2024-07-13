using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCloseArea : MonoBehaviour
{
    [SerializeField] Door door;
    [SerializeField] Puzzle nextPuzzle;

    private void CloseDoor()
    {
        if (door == null) return;

        if (door.State == Door.EState.Open) door.ToggleDoor();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            CloseDoor();
            nextPuzzle?.BeginPuzzle();
        }
    }
}
