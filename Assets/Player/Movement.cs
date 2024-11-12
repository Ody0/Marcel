using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody rb;

    Vector2 movInput;
    float speed = 5f;

    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public Animator anims;
    bool isMoving;

    bool isRunning;

    public Transform groundCheck;

    public Transform cameraTransform;
    public Transform modelTransform; // Reference to the model you want to rotate for slope
    public float slopeRotationSpeed = 10f;
    public float groundCheckDistance = 1.2f; // Distance to check below the player for ground

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        movInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        isRunning = Input.GetKey(KeyCode.LeftShift);

        speed = isRunning ? runSpeed : walkSpeed;

        Move();
        PlayerRotation();
        ModelSlopeAlignment(); // Call the slope alignment function for the model
        Anims();
    }

    private void Move()
    {
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0;
        camRight.y = 0;

        Vector3 moveDirection = (camForward * movInput.y + camRight * movInput.x).normalized;
        rb.velocity = new Vector3(moveDirection.x * speed, rb.velocity.y, moveDirection.z * speed);
    }

    private void PlayerRotation()
    {
        if (isMoving)
        {
            // Calculate movement direction
            Vector3 moveDirection = new Vector3(movInput.x, 0, movInput.y).normalized;
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;

            camForward.y = 0;
            camRight.y = 0;

            Vector3 desiredDirection = (camForward * moveDirection.z + camRight * moveDirection.x).normalized;

            // Rotate the main player object to face the movement direction
            Quaternion targetRotation = Quaternion.LookRotation(desiredDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * slopeRotationSpeed);
        }
    }

    private void ModelSlopeAlignment()
    {
        RaycastHit hit;
        if (Physics.Raycast(groundCheck.position, Vector3.down, out hit, groundCheckDistance))
        {
            // Use the ground's normal to adjust the model's rotation on slopes
            Vector3 groundNormal = hit.normal;
            Quaternion slopeRotation = Quaternion.FromToRotation(modelTransform.up, groundNormal) * modelTransform.rotation;

            // Smoothly rotate the model to match the ground's slope
            modelTransform.rotation = Quaternion.Slerp(modelTransform.rotation, slopeRotation, Time.deltaTime * slopeRotationSpeed);
        }
    }

    private void Anims()
    {
        isMoving = rb.velocity.magnitude > 0.2f;
        anims.SetBool("walking", isMoving);
        anims.SetBool("running", isRunning);
    }
}
