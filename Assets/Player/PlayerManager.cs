using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public GameObject PauseMenu;

    public GameObject buildMenu;

    public Movement move;

    public GameObject camMan;

    public static PlayerManager Instance;

    public GameObject RestUI;

    public void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            camMan.SetActive(false);

            PauseMenu.SetActive(true);
            move.canPlay = false;

            RestUI.SetActive(false);
        }


        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (buildMenu.activeSelf == false)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                camMan.SetActive(false);

                buildMenu.SetActive(true);
                move.canPlay = false;
            }
            else
            {
                CloseShop();
            }
        }



    }

    public void CloseShop()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        camMan.SetActive(true);

        buildMenu.SetActive(false);
        move.canPlay = true;
    }
}
