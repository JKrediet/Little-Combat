﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public bool attack;
    public AudioSource source;
    public AudioClip foot1, foot2;

    private Animator anim;

    public GameObject moveTool;
    public GameObject sword;
    public GameObject gunno;
    private bool isAiming;

    public AudioClip[] painClips;

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
            if (!isAiming)
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
            sword.SetActive(true);
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
        sword.SetActive(false);
    }

    public void FootOne()
    {
        //source.Stop();
        source.clip = foot1;
        source.Play();
    }

    public void FootTwo()
    {
        //source.Stop();
        source.clip = foot2;
        source.Play();
    }
    public void PainSound()
    {
        int i = 0;
        i = Random.Range(0, 3);

        source.Stop();
        source.clip = painClips[i];
        source.Play();
    }

    public void StopMoving()
    {
        anim.SetInteger("PlayerState", 0);
    }
    public void ShieldToggle(bool _value)
    {
        anim.SetBool("isShielding", _value);
    }
    public void ShieldWalking(bool _value)
    {
        anim.SetBool("isShieldMoving", _value);
    }
    public void AimToggle(bool _value)
    {
        anim.SetBool("isAiming", _value);
        gunno.SetActive(_value);
        isAiming = _value;
    }
    public void GunAttack()
    {
        anim.SetBool("IsAttacking", true);
    }
    public void StopGunAttack()
    {
        anim.SetBool("IsAttacking", false);
    }
    public void Death()
    {
        anim.SetBool("isDood", true);
    }
    public void Shush()
    {
        anim.SetBool("isDood", false);
    }
    public void NotTakingShieldDamageAnymore()
    {
        anim.SetBool("isTakingShieldDamage", false);
        FindObjectOfType<PlayerMovement>().StopBlocking();
    }
    public void IsTakingShieldDamage()
    {
        anim.SetBool("isTakingShieldDamage", true);
    }
    public void IsTakingDamage()
    {
        anim.SetBool("isTakingDamage", true);
    }
    public void NotTakingDamageAnymore()
    {
        anim.SetBool("isTakingDamage", false);
        FindObjectOfType<PlayerMovement>().StopTakingDamage();
    }
}
