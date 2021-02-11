using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed, gravity, jumpForce, minCrouchHeight, maxCrouchHeight, CameraRotationSpeed, playerDamage;
    public Transform cameraReference, cameraFollow;

    //shitload aan testdingen, please no remove!
    public bool status_Push, status_pickup, isHoldingLaser, isHoldingPickup;

    //privates
    private Quaternion targetRotation;
    public CharacterController controller;
    private Vector3 moveDir, movement;
    private float downForce, targetAngle, startSpeed;
    private Vector2 rotation;
    private float turnSmoothBelocity;

    //attack
    public bool isAttacking;
    private float nextAttack, attackCooldown;

    //movement collision check
    private bool mayNotMoveForward;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        startSpeed = speed;
    }

    private void FixedUpdate()
    {
        Movement();
        Gravity();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (controller.isGrounded)
            {
                if (!status_Push)
                {
                    if (!isHoldingPickup)
                    {
                        if (Time.time > nextAttack)
                        {
                            nextAttack = Time.time + attackCooldown;
                            FindObjectOfType<AnimationController>().Attack();
                        }
                    }
                    
                }
            }
        }
    }

    public void Movement()
    {
        if (!isAttacking)
        {
            //if not pushing something, use these movement controls
            if (!status_Push)
            {
                //recieve input for playerdirection
                if (controller.isGrounded)
                {
                    moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
                    //moveDir = transform.TransformDirection(moveDir);
                    //rotate player to camera forward on input
                    if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
                    {
                        speed = startSpeed;
                        if (Input.GetButton("Fire2"))
                        {
                            speed = startSpeed / 2;
                        }
                        targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg + cameraReference.eulerAngles.y;
                        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothBelocity, 0.05f);
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
            }
            //push object
            if (status_Push)
            {
                speed = startSpeed / 2;

                //recieve input for playerdirection
                moveDir = new Vector3(0, 0, Input.GetAxisRaw("Vertical")).normalized;
                moveDir *= speed;
                moveDir = transform.TransformDirection(moveDir);

                
                transform.Rotate(0, Input.GetAxisRaw("Horizontal"), 0, Space.Self);

                //movement
                controller.Move(moveDir / 2 * Time.deltaTime);
                controller.Move(new Vector3(0, downForce, 0) * Time.deltaTime);
                cameraFollow.position = transform.position;
            }
        }
    }
    private void LateUpdate()
    {
        //set to camera forward
        if (isHoldingLaser)
        {
            transform.forward = new Vector3(cameraReference.forward.x, transform.forward.y, cameraReference.forward.z);
        }
    }
    public void Gravity()
    {
        if(!controller.isGrounded)
        {
            downForce -= gravity * Time.deltaTime;
        }
    }
    public void BasicAttack()
    {
        //actual attack
        Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward, 1);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                if (collider.GetComponent<EnemyHealth>())
                {
                    collider.GetComponent<EnemyHealth>().GiveDamage(playerDamage);
                }
            }
        }
    }
    private void OnCollisionEnter(Collision hit)
    {
        if (hit.transform.tag == "Pickup")
        {
            if (Input.GetButton("Fire2"))
            {
                //wekrt neit
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 0.6f;
        Gizmos.DrawRay(transform.position, direction);

        Gizmos.DrawSphere(transform.position + transform.forward, 0.5f);
    }
}
