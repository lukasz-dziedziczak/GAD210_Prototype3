using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PauseMenu : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    private void OnResumeButtonPress()
    {
        UI.Instance.OnPausePress();
    }

    private void OnExitButtonPress()
    {
        Application.Quit();
    }
}
