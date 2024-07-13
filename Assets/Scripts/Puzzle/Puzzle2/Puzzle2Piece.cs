using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle2Piece : MonoBehaviour
{
    [SerializeField] int index = 0;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Puzzle2 puzzle2;

    public int Index => index;


    private void Awake()
    {
        if (puzzle2 == null) puzzle2 = GetComponentInParent<Puzzle2>();
    }

    public void PlaySound()
    {
        audioSource.Play();
    }

    public void StopSound()
    {
        audioSource.Stop();
    }

    public void Interaction()
    {
        if (puzzle2 != null)
        {
            puzzle2.ActivatePuzzlePiece(index);
        }
    }
}
