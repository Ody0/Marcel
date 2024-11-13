using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{


    // ce script est dans un parant sur la position du joueur, la camera est enfant de ce parent

    public Transform player;
    public float rotationSpeed = 5f;
    public Vector2 mouseInput;

    float rot_y;

    float rot_x;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        mouseInput = new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));

        rot_y = rot_y += mouseInput.y;

        rot_x = rot_x += mouseInput.x;

        transform.rotation = Quaternion.Euler(rot_x, rot_y, 0);
    }

    void LateUpdate()
    {
        transform.position = player.position;
    }

}