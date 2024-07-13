using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1 : Puzzle
{
    [SerializeField] Puzzle1PieceSlot[] slots;
    [SerializeField] AudioClip[] puzzlePieces;
    [SerializeField] float maxTimeBetweenPieces = 1.0f;
    [SerializeField] Door door;

    AudioSource CurrentlyPlaying;
    List<AudioSource> AudioQueue = new List<AudioSource>();

    [Header("Debug")]
    [SerializeField] int solutionIndex;
    [SerializeField] float timeSinceLastPlay;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (solutionIndex == puzzlePieces.Length)
        {
            if (!CurrentlyPlaying.isPlaying)
            {
                Debug.Log("Puzzle solved");
                solutionIndex = 0;
                if (door != null) door.ToggleDoor();
            }

            return;
        }

        timeSinceLastPlay += Time.deltaTime;

        if ((CurrentlyPlaying == null || !CurrentlyPlaying.isPlaying) && AudioQueue.Count > 0)
        {
            CurrentlyPlaying = AudioQueue[0];
            AudioQueue.RemoveAt(0);

            CurrentlyPlaying.Play();
            timeSinceLastPlay = -CurrentlyPlaying.clip.length;

            if (CurrentlyPlaying == slots[solutionIndex].AudioSource && CurrentlyPlaying.clip == puzzlePieces[solutionIndex])
            {
                solutionIndex++;
                Debug.Log("Correct piece " + solutionIndex + " in slot and played");
            }
            else solutionIndex = 0;
        }

        if (solutionIndex != 0 && timeSinceLastPlay > maxTimeBetweenPieces)
        {
            print("ran out of time, resetting");
            solutionIndex = 0;
        }
    }

    public void AddToQueue(AudioSource audioSource)
    {
        if (!AudioQueue.Contains(audioSource))
        {
            AudioQueue.Add(audioSource);
            Debug.Log("Added " + audioSource + " to queue");
        }
        
    }

    public override void BeginPuzzle()
    {
        solutionIndex = 0;
    }
}
