using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] float closedPosition;
    [SerializeField] float openPosition;
    [SerializeField] float changeTime;
    [SerializeField] AudioSource audioSource;

    float changeStartTime;
    float timeSinceStart => Time.time - changeStartTime;
    EState state;

    public EState State => state;

    public enum EState
    {
        Closed,
        Open,
        Opening,
        Closeing
    }

    private void Update()
    {
        if (state == EState.Opening)
        {
            if (timeSinceStart < changeTime)
            {
                float currentPosition = Mathf.Lerp(closedPosition, openPosition, timeSinceStart / changeTime);
                transform.position = new Vector3(transform.position.x, currentPosition, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, openPosition, transform.position.z);
                state = EState.Open;
            }
        }

        else if (state == EState.Closeing)
        {
            if (timeSinceStart < changeTime)
            {
                float currentPosition = Mathf.Lerp(openPosition, closedPosition, timeSinceStart / changeTime);
                transform.position = new Vector3(transform.position.x, currentPosition, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, closedPosition, transform.position.z);
                state = EState.Closed;
            }
        }
    }

    public void ToggleDoor()
    {
        if (state == EState.Open)
        {
            changeStartTime = Time.time;
            state = EState.Closeing;
        }

        else if (state == EState.Closed)
        {
            changeStartTime = Time.time;
            state = EState.Opening;
        }

        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
    }
}
