﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed, gravity, jumpForce, minCrouchHeight, maxCrouchHeight, CameraRotationSpeed;
    public Transform cameraReference, cameraFollow;
    public bool status_Push, push_forward, push_right, push_left, push_any, isHoldingLaser;

    //privates
    private Quaternion targetRotation;
    public CharacterController controller;
    private Vector3 moveDir, movement;
    private float downForce, rotationTime, targetAngle, startSpeed;
    private Vector2 rotation;
    private float turnSmoothBelocity;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        startSpeed = speed;
    }

    private void Update()
    {
        Movement();
        Gravity();
        Crouch();
    }

    public void Movement()
    {
        //if not pushing something, use these movement controls
        if (!status_Push)
        {
            //recieve input for playerdirection
            if(controller.isGrounded)
            {
                moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
                //moveDir = transform.TransformDirection(moveDir);
                //rotate player to camera forward on input
                if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
                {
                    speed = startSpeed;
                    targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg + cameraReference.eulerAngles.y;
                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothBelocity, 0.1f);
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);
                }
                else
                {
                    speed = 0;
                }
            }

            //movement
            movement = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(movement * speed * Time.deltaTime);
            controller.Move(new Vector3(0, downForce, 0) * Time.deltaTime);
            cameraFollow.position = transform.position;

            //set to camera forward
            if (isHoldingLaser)
            {
                Vector3 forward = new Vector3(cameraReference.forward.x, transform.forward.y, cameraReference.forward.z);
            }
        }
        if(status_Push)
        {
            //recieve input for playerdirection
            moveDir = new Vector3(0, 0, Input.GetAxisRaw("Vertical")).normalized;
            moveDir *= speed;
            moveDir = transform.TransformDirection(moveDir);

            //rotation
            //check if you may rotate/move in given direction and limit movement
            if(push_forward)
            {
                push_any = true;
                moveDir.x = Mathf.Clamp(moveDir.z, 0, -1);
            }
            if(push_right)
            {
                push_any = true;
                float tempInput = Input.GetAxisRaw("Horizontal");
                tempInput = Mathf.Clamp(tempInput, -1, 0);
                transform.Rotate(0, tempInput, 0, Space.Self);
            }
            if(push_left)
            {
                push_any = true;
                float tempInput = Input.GetAxisRaw("Horizontal");
                tempInput = Mathf.Clamp(tempInput, 0, 1);
                transform.Rotate(0, tempInput, 0, Space.Self);
            }
            //none are true
            if(!push_forward && !push_left && !push_right)
            {
                push_any = false;
            }
            if (!push_any)
            {
                transform.Rotate(0, Input.GetAxisRaw("Horizontal"), 0, Space.Self);
            }

            //movement
            controller.Move(moveDir / 2 * Time.deltaTime);
            controller.Move(new Vector3(0, downForce, 0) * Time.deltaTime);
            cameraFollow.position = transform.position;

        }
    }
    public void Gravity()
    {
        if(controller.isGrounded)
        {
            if (!status_Push)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    downForce = jumpForce;
                }
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


    //dit is reserve....
    
    ////rotate player to camera forward on input
    //        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical") || Input.GetButton("Fire1") || isHoldingLaser)
    //        {
    //            //get forward
    //            Vector3 forward = new Vector3(cameraReference.forward.x, transform.forward.y, cameraReference.forward.z);
    //targetRotation = Quaternion.LookRotation(forward);

    //            //early smooth, faster the longer mousebutton is hold
    //            rotationTime += Time.deltaTime* CameraRotationSpeed;

    ////rotate toward forward overtime
    //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationTime);
    //        }
    //        else
    //        {
    //            rotationTime = 0;
    //        }
}
