using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public static UI Instance;

    [field: SerializeField] public UI_PauseMenu PauseMenu {  get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        
    }

    private void Start()
    {
        Player.Instance.Input.OnPausePress += OnPausePress;
    }

    private void OnDisable()
    {
        Player.Instance.Input.OnPausePress -= OnPausePress;
    }

    public static void ShowCursor(bool showing)
    {
        Cursor.lockState = showing ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = showing;
    }

    public void OnPausePress()
    {
        if (!PauseMenu.gameObject.activeSelf)
        {
            PauseMenu.gameObject.SetActive(true);
        }
        else
        {
            PauseMenu.gameObject.SetActive(false);
        }
    }
}
