using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_PauseMenu : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0;
        UI.ShowCursor(true);
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        UI.ShowCursor(false);
    }

    public void OnResumeButtonPress()
    {
        UI.Instance.OnPausePress();
    }

    public void OnResetButtonPress()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnExitButtonPress()
    {
        Application.Quit();
    }
}
