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
            if(PauseMenu.activeSelf == false)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                camMan.SetActive(false);

                PauseMenu.SetActive(true);
                move.canPlay = false;

                RestUI.SetActive(false);
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                camMan.SetActive(true);

                PauseMenu.SetActive(false);
                move.canPlay = true;

                RestUI.SetActive(true);
            }
        }


        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (buildMenu.activeSelf == false)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                camMan.SetActive(false);

                buildMenu.SetActive(true);
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
    }
}
