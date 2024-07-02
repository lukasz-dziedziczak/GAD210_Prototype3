using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{

    public static void ShowCursor(bool showing)
    {
        Cursor.lockState = showing ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = showing;
    }
}
