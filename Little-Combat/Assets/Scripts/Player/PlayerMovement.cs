using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed, gravity, jumpForce, minCrouchHeight, maxCrouchHeight, rotationSpeed;
    public Transform cameraReference, cameraFollow;
    private Quaternion targetRotation;

    //camera
    public float lookSpeed;

    //privates
    private CharacterController controller;
    private Vector3 moveDir;
    private float downForce, rotationTime;
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

        //movement
        controller.Move(moveDir * Time.deltaTime);
        controller.Move(new Vector3(0, downForce, 0) * Time.deltaTime);
        cameraFollow.position = transform.position;

        //rotate player to camera forward on input
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical") || (Input.GetButton("Fire1")))
        {
            //get forward
            Vector3 forward = new Vector3(cameraReference.forward.x, transform.forward.y, cameraReference.forward.z);
            targetRotation = Quaternion.LookRotation(forward);

            //early smooth, faster the longer mousebutton is hold
            rotationTime += Time.deltaTime * rotationSpeed;

            //rotate toward forward overtime
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationTime);
        }
        else
        {
            rotationTime = 0;
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
