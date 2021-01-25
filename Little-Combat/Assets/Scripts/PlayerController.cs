using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed, gravity, jumpForce;

    //privates
    private CharacterController controller;
    private Vector3 moveDir;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void Movement()
    {
        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.z = Input.GetAxisRaw("Vertical");

        controller.Move(moveDir.normalized * speed * Time.deltaTime);
    }
    public void Gravity()
    {

    }

    private void Update()
    {
        Movement();
    }
}
