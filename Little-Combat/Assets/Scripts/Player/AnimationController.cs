using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public bool attack;
    private Animator anim;
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
                    anim.SetInteger("PlayerState", 2);
                }
                //walk
                else
                {
                    anim.SetInteger("PlayerState", 1);
                }
            }
            else
            {
                if (Input.GetButton("Fire2"))
                {
                    anim.SetInteger("PlayerState", 3);
                }
                //walk
                else
                {
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
        //stuff that needs to happen for attack
        StopMoving();
        FindObjectOfType<PlayerMovement>().isAttacking = attack;
        attack = true;
        anim.SetBool("IsAttacking", attack);

        //actual attack here
        FindObjectOfType<PlayerMovement>().BasicAttack();
    }
    public void StopAttack()
    {
        attack = false;
        anim.SetBool("IsAttacking", attack);
        FindObjectOfType<PlayerMovement>().isAttacking = attack;
    }
    public void StopMoving()
    {
        anim.SetInteger("PlayerState", 0);
    }
}
