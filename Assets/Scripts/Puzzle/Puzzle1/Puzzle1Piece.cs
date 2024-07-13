using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1Piece : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] Rigidbody rb;
    [SerializeField] Collider _collider;

    public Puzzle1PieceSlot Slot;
    public AudioSource AudioSource => audioSource;

    public bool InSlot => Slot != null;

    public void PickUp()
    {
        if (Slot != null)
        {
            Slot.RemoveFromSlot();
            Slot = null;
        }

        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        _collider.isTrigger = true;

        if (audioSource.isPlaying) audioSource.Stop();

        Debug.Log("Picked up " + name);
    }

    public void Drop()
    {
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
        _collider.isTrigger = false;

        Debug.Log("Dropped " + name);
    }

    public void InsertIntoSlot(Puzzle1PieceSlot newSlot)
    {
        Slot = newSlot;

        transform.parent = Slot.transform;
        transform.localPosition = Slot.PuzzlePiecePosition;
        transform.localEulerAngles = Vector3.zero;

        Debug.Log("Inserted " + name + " into " + Slot.name);
    }

    public void PlaySound()
    {
        /*if (audioSource != null)
        {
            audioSource.Play();
        }*/

        if (Slot != null)
        {
            Slot.Puzzle.AddToQueue(audioSource);
        }
    }
}
