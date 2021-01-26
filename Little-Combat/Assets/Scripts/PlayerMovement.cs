using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed, gravity, jumpForce, minCrouchHeight, maxCrouchHeight;
    public Transform cameraRotation, cameraFollow;

    //camera
    public float lookSpeed;

    //privates
    private CharacterController controller;
    private Vector3 moveDir;
    private float downForce;
    private Vector2 rotation;


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
        //recieve input for playerdirection
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        moveDir *= speed;
        moveDir = transform.TransformDirection(moveDir);

        //controller
        controller.Move(moveDir * Time.deltaTime);
        controller.Move(new Vector3(0, downForce, 0) * Time.deltaTime);
        cameraFollow.position = transform.position;

        //player forward on movement
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            transform.forward = new Vector3(cameraRotation.forward.x, transform.forward.y, cameraRotation.forward.z);
        }
    }
    public void Gravity()
    {
        if(controller.isGrounded)
        {
            //downForce = -0.1f;
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
        //if (Input.GetKey(KeyCode.LeftControl))
        //{
        //    controller.height = minCrouchHeight;
        //}
        //else
        //{
        //    controller.height = maxCrouchHeight;
        //}
    }
}
