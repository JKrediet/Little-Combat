using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed, gravity, jumpForce, minCrouchHeight, maxCrouchHeight;

    //privates
    private CharacterController controller;
    private Vector3 moveDir;
    private float downForce, groundedCooldown = 0.1f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Movement();
        Gravity();
        Crouch();
    }

    public void Movement()
    {
        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.z = Input.GetAxisRaw("Vertical");

        controller.Move(moveDir.normalized * speed * Time.deltaTime);
        controller.Move(new Vector3(0, downForce, 0) * Time.deltaTime);
    }
    public void Gravity()
    {
        if(controller.isGrounded)
        {
            if (Time.time >= groundedCooldown + Time.time)
            {
                downForce = -0.1f;
            }
            if (Input.GetButtonDown("Jump"))
            {
                downForce = jumpForce;
            }
        }
        else
        {
            downForce -= gravity * Time.deltaTime;
        }
    }
    public void Crouch()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            controller.height = minCrouchHeight;
        }
        else
        {
            controller.height = maxCrouchHeight;
        }
    }
}
