using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle2Piece : MonoBehaviour
{
    [SerializeField] int index = 0;
    [SerializeField] AudioSource audioSource;

    public int Index => index;

    public void PlaySound()
    {
        audioSource.Play();
    }

    public void StopSound()
    {
        audioSource.Stop();
    }
}
