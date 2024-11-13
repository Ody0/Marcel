using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public GameObject canvas;

    public Movement movement;

    public GameObject camMan;

    public GameObject RestUI;

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        movement.canPlay = true;
        RestUI.SetActive(true);
        camMan.SetActive(true);
        canvas.SetActive(false);
    }
}
