using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public bool attack;
    private Animator anim;

    public GameObject moveTool;
    public GameObject gunno;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        //player model to playercontroller forward
        transform.TransformDirection(FindObjectOfType<PlayerMovement>().gameObject.transform.forward);

        //idle
        if (!attack)
        {
            if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
            {
                //pickup
                if (Input.GetButton("Fire2"))
                {
                    moveTool.SetActive(true);
                    anim.SetInteger("PlayerState", 2);
                }
                //walk
                else
                {
                    moveTool.SetActive(false);
                    anim.SetInteger("PlayerState", 1);
                }
            }
            else
            {
                if (Input.GetButton("Fire2"))
                {
                    moveTool.SetActive(true);
                    anim.SetInteger("PlayerState", 3);
                }
                //walk
                else
                {
                    moveTool.SetActive(false);
                    StopMoving();
                }
            }
            if (!FindObjectOfType<PlayerMovement>().controller.isGrounded)
            {
                anim.SetBool("Jump", true);
            }
        }
    }
    private void LateUpdate()
    {
        if (FindObjectOfType<PlayerMovement>().controller.isGrounded)
        {
            anim.SetBool("Jump", false);
        }
    }
    public void Attack()
    {
        if (attack == false)
        {
            //stuff that needs to happen for attack
            StopMoving();
            attack = true;
            FindObjectOfType<PlayerMovement>().isAttacking = attack;
            anim.SetBool("IsAttacking", attack);
            gunno.SetActive(true);
        }
    }
    public void ActualAttack()
    {
        //actual attack here
        FindObjectOfType<PlayerMovement>().BasicAttack();
    }
    public void StopAttack()
    {
        attack = false;
        FindObjectOfType<PlayerMovement>().isAttacking = attack;
        anim.SetBool("IsAttacking", attack);
        gunno.SetActive(false);
    }
    public void StopMoving()
    {
        anim.SetInteger("PlayerState", 0);
    }
    public void AimToggle(bool _value)
    {
        anim.SetBool("isAiming", _value);
    }
}
