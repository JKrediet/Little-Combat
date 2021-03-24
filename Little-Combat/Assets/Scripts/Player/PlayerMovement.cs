using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed, gravity, jumpForce, CameraRotationSpeed, playerDamage, attackCooldown;
    public Transform cameraReference, cameraFollow, shield;
    public AudioSource source;
    public AudioClip swordSoundClip;
    public AudioClip foot1, foot2;

    //shitload aan testdingen, please no remove!
    public bool status_Push, status_pickup, isHoldingLaser, isHoldingPickup, status_gun, isDead, status_shield, shieldMoving, blocking, isTakingDamage; //love em!
    public Image crosshair;

    //privates
    private Quaternion targetRotation;
    public CharacterController controller;
    private Vector3 moveDir, movement;
    private float downForce, targetAngle, startSpeed;
    private Vector2 rotation;
    private float turnSmoothBelocity;

    //attack
    public bool isAttacking;
    private float nextAttack;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        startSpeed = speed;
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            if (!blocking)
            {
                if (!isTakingDamage)
                {
                    Movement();
                    Gravity();
                }
            }
        }
    }
    private void Update()
    {
        if(status_gun)
        {
            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            {
                Aim();
            }
        }
        if (!blocking)
        {
            if (!isTakingDamage)
            {
                if (!Input.GetButton("Fire2"))
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        if (controller.isGrounded)
                        {
                            if (!status_Push)
                            {
                                if (!isHoldingPickup)
                                {
                                    if (!status_shield)
                                    {
                                        if (Time.time > nextAttack)
                                        {
                                            if (status_gun)
                                            {
                                                nextAttack = Time.time + attackCooldown;
                                                GetComponent<MoveObjects>().FireGun();
                                                FindObjectOfType<AnimationController>().GunAttack();
                                            }
                                            else
                                            {
                                                nextAttack = Time.time + attackCooldown;
                                                FindObjectOfType<AnimationController>().Attack();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (Input.GetButtonDown("Aim"))
                    {
                        if (!isAttacking)
                        {
                            if (!status_shield)
                            {
                                Aim();
                            }
                            else
                            {
                                ToShield();
                                Aim();
                            }
                        }
                    }
                    if (Input.GetButtonDown("Shield"))
                    {
                        if (!isAttacking)
                        {
                            if (!status_gun)
                            {
                                ToShield();
                            }
                            else
                            {
                                Aim();
                                ToShield();
                            }
                        }
                    }
                }
            }
        }
    }
    public void Aim()
    {
        crosshair.gameObject.SetActive(!crosshair.gameObject.activeSelf);
        status_gun = !status_gun;
        FindObjectOfType<AnimationController>().AimToggle(status_gun);
        cameraReference.GetComponent<CameraContrller>().AimToggle(status_gun);
        GetComponent<MoveObjects>().isHoldingGun = status_gun;
    }
    public void ToShield()
    {
        status_shield = !status_shield;
        shield.gameObject.SetActive(status_shield);
        FindObjectOfType<AnimationController>().ShieldToggle(status_shield);
        cameraReference.GetComponent<CameraContrller>().ShieldToggle(status_shield);
        GetComponent<MoveObjects>().isShielding = status_shield;
    }

    public void Movement()
    {
        if (!isAttacking)
        {
            if (!status_gun)
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
                            //shhhh het ging me te sloom      <------
                            if (Input.GetButton("Running"))
                            {
                                speed = startSpeed * 3;
                            }
                            else
                            {
                                speed = startSpeed;
                            }
                            //dit haal ik later weg hiertussen <------
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
                    if (moveDir.magnitude > 0)
                    {
                        shieldMoving = true;
                        FindObjectOfType<AnimationController>().ShieldWalking(shieldMoving);
                    }
                    else
                    {
                        shieldMoving = false;
                        FindObjectOfType<AnimationController>().ShieldWalking(shieldMoving);
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
    }
    private void LateUpdate()
    {
        //set to camera forward
        if (isHoldingLaser || status_gun)
        {
            transform.forward = new Vector3(cameraReference.forward.x, transform.forward.y, cameraReference.forward.z);
        }
    }
    public void Gravity()
    {
        if (!controller.isGrounded)
        {
            downForce -= gravity * Time.deltaTime;
        }
        else
        {
            downForce = -gravity;
        }
    }
    public void BasicAttack()
    {
        source.Stop();
        source.clip = swordSoundClip;
        source.Play();

        //actual attack
        Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward, 1);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                if (collider.GetComponent<BaseEnemy>())
                {
                    collider.GetComponent<BaseEnemy>().GiveDamage(playerDamage);
                }
            }
        }
    }
    public void FireBallHit()
    {
        blocking = true;
        FindObjectOfType<AnimationController>().IsTakingShieldDamage();
    }
    public void StopBlocking()
    {
        blocking = false;
    }
    public void StopTakingDamage()
    {
        isTakingDamage = false;
    }

}
