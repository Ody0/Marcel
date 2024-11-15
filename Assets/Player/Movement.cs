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

    private bool isGrounded;

    public bool canPlay = true;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (canPlay)
        {
            movInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            isRunning = Input.GetKey(KeyCode.LeftShift);
            speed = isRunning ? runSpeed : walkSpeed;

            Move();
            Jump();
            PlayerRotation();
            ModelSlopeAlignment();
            Anims();
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    public void FixedUpdate()
    {
        Move();
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

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            anims.SetTrigger("jump");
            rb.velocity = rb.transform.up * 12;
        }
    }

    private void PlayerRotation()
    {
        if (isMoving)
        {
            // Calculate movement direction based on input and camera
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;

            camForward.y = 0;
            camRight.y = 0;

            Vector3 moveDirection = new Vector3(movInput.x, 0, movInput.y).normalized;
            Vector3 desiredDirection = (camForward * moveDirection.z + camRight * moveDirection.x).normalized;

            // Rotate only on the Y-axis based on movement direction
            Quaternion targetRotation = Quaternion.LookRotation(desiredDirection);
            rb.transform.rotation = Quaternion.Slerp(rb.transform.rotation, targetRotation, Time.deltaTime * slopeRotationSpeed);
        }
    }

    private void ModelSlopeAlignment()
    {
        RaycastHit hit;
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, out hit, groundCheckDistance);

        if (isGrounded)
        {
            // Align model to ground slope (X and Z alignment)
            Vector3 groundNormal = hit.normal;
            Quaternion slopeRotation = Quaternion.FromToRotation(modelTransform.up, groundNormal) * modelTransform.rotation;

            // Lock the Y rotation to match the player's facing direction
            float fixedYRotation = modelTransform.eulerAngles.y;
            slopeRotation = Quaternion.Euler(slopeRotation.eulerAngles.x, fixedYRotation, slopeRotation.eulerAngles.z);

            // Smoothly rotate the model to match the ground's slope while keeping Y-axis fixed
            modelTransform.rotation = Quaternion.Slerp(modelTransform.rotation, slopeRotation, Time.deltaTime * slopeRotationSpeed);
        }
        else
        {
            // Reset X and Z rotation when in the air, and lock Y rotation
            Quaternion targetRotation = Quaternion.Euler(0, modelTransform.eulerAngles.y, 0);
            modelTransform.rotation = Quaternion.Slerp(modelTransform.rotation, targetRotation, Time.deltaTime * slopeRotationSpeed);
        }
    }


    private void Anims()
    {
        isMoving = rb.velocity.magnitude > 0.2f;
        anims.SetBool("isGrounded", isGrounded);
        anims.SetBool("walking", isMoving);
        anims.SetBool("running", isRunning);
    }
}
