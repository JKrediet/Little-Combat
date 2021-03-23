﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss2 : BaseEnemy
{
    public LayerMask playerMask;
    public int currentAttack;
    public Rigidbody fireBall;
    public float projectileSpeed, timeBeforeFireBall, shieldHealh;
    private float countDown;
    private bool inThirdStage, inSecondStage, isChangingStance;
    public Transform aim2, aim3, bzoop, aura;

    public AudioSource source;
    public AudioClip footStep001, footStep002, attackSound, scream, beat;

    protected override void Start()
    {
        base.Start();
        countDown = timeBeforeFireBall;
        shieldHealh = 1;
        aura.gameObject.SetActive(true);
    }
    protected override void Attack()
    {
        base.Attack();
        anim.SetInteger("currentAttack", currentAttack);
        agent.isStopped = true;
    }
    public override void DoneAttacking()
    {
        anim.SetInteger("currentAttack", 0);
        base.DoneAttacking();
        currentAttack = 0;
        countDown = timeBeforeFireBall;
    }
    protected override void AnimationThings()
    {
        anim.SetBool("isIdle", idle);

        if (health <= 0f)
        {
            agent.isStopped = true;
            anim.SetBool("isDead", true);
            bossDead = true;
        }
    }

    public void Footstep(int i)
    {
        source.Stop();

        if(i == 1)
        {
            source.clip = footStep001;
        }else if(i == 2)
        {
            source.clip = footStep002;
        }else if(i == 3)
        {
            source.clip = attackSound;
        }else if(i == 4)
        {
            source.clip = scream;
        }else if(i == 5)
        {
            source.clip = beat;
        }

        source.Play();
    }

    protected override void Movement()
    {
        if (playerInRange)
        {
            if (countDown > 0)
            {
                countDown -= Time.deltaTime;
            }
            else
            {
                currentAttack = 2;
            }
            if (!isChangingStance)
            {
                if(isAttacking)
                {
                    agent.SetDestination(transform.position);
                }
                if (!isAttacking)
                {
                    if (currentAttack == 0)
                    {
                        agent.isStopped = false;
                        currentAttack = Random.Range(1, 3);
                    }
                    else if (currentAttack == 1)
                    {
                        if (inThirdStage)
                        {
                            currentAttack = 2;
                        }
                        if (targetDistance <= attackRange)
                        {
                            if (Time.time >= nextAttack)
                            {
                                idle = false;
                                nextAttack = Time.time + attackCooldown;
                                Attack();
                            }
                            else
                            {
                                idle = true;
                            }
                        }
                        else
                        {
                            if (Time.time >= nextAttack)
                            {
                                idle = false;
                            }
                            else
                            {
                                idle = true;
                            }
                            if (!idle)
                            {
                                agent.SetDestination(player.transform.position - transform.forward);
                            }
                        }
                    }
                    else if (currentAttack == 2)
                    {
                        if (targetDistance <= rangedAttackRange)
                        {
                            if (Time.time >= nextAttack)
                            {
                                idle = false;
                                nextAttack = Time.time + attackCooldown;
                                Attack();
                            }
                            else
                            {
                                idle = true;
                            }
                            if (!idle)
                            {
                                agent.SetDestination(player.transform.position - transform.forward * (attackRange / 2));
                            }
                        }
                        else
                        {
                            if (Time.time >= nextAttack)
                            {
                                idle = false;
                            }
                            else
                            {
                                idle = true;
                            }
                            if (!idle)
                            {
                                agent.SetDestination(player.transform.position - transform.forward * (attackRange / 2));
                            }
                        }
                    }
                }
                else
                {
                    Vector3 direction = (player.transform.position - transform.position).normalized;
                    transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                }
            }
        }
    }
    public void AttackHitbox()
    {
        //actual attack
        Collider[] colliders = Physics.OverlapBox(transform.position + transform.forward * 2, new Vector3(2, 15, 2));
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                //Debug.Log(collider.transform.name);

                if (collider.GetComponent<PlayerHealth>())
                {
                    collider.GetComponent<PlayerHealth>().GiveDamage(attackDamage);
                }
                else if (collider.GetComponent<ObjectHealth>())
                {
                    collider.GetComponent<ObjectHealth>().DoDamage();
                }
            }
        }
    }
    public void FireBall()
    {
        anim.SetInteger("currentAttack", currentAttack);
        Rigidbody fire = Instantiate(fireBall, transform.position + transform.up, transform.rotation);
        fire.velocity = fire.transform.forward * projectileSpeed;
        if(inSecondStage)
        {
            Vector3 direction4 = (aim2.transform.position - transform.position).normalized;
            Rigidbody fire4 = Instantiate(fireBall, transform.position + transform.up, Quaternion.LookRotation(new Vector3(direction4.x, 0, direction4.z)));
            fire4.velocity = fire4.transform.forward * projectileSpeed;

            Vector3 direction5 = (aim3.transform.position - transform.position).normalized;
            Rigidbody fire5 = Instantiate(fireBall, transform.position + transform.up, Quaternion.LookRotation(new Vector3(direction5.x, 0, direction5.z)));
            fire5.velocity = fire5.transform.forward * projectileSpeed;
        }
        if(inThirdStage)
        {
            Vector3 direction2 = (aim2.transform.position - transform.position).normalized;
            Rigidbody fire2 = Instantiate(fireBall, transform.position + transform.up, Quaternion.LookRotation(new Vector3(direction2.x, 0, direction2.z)));
            fire2.velocity = fire2.transform.forward * projectileSpeed;

            Vector3 direction3 = (aim3.transform.position - transform.position).normalized;
            Rigidbody fire3 = Instantiate(fireBall, transform.position + transform.up, Quaternion.LookRotation(new Vector3(direction3.x, 0, direction3.z)));
            fire3.velocity = fire3.transform.forward * projectileSpeed;
        }
    }
    public void ToSecondStage()
    {
        DoneAttacking();
        anim.SetInteger("currentPhase", 1);
        shieldHealh = 2;
        agent.speed = 2.5f;
        inSecondStage = true;
        agent.isStopped = true;
    }
    public void ToThirdStage()
    {
        DoneAttacking();
        anim.SetInteger("currentPhase", 2);
        attackRange = 4;
        shieldHealh = 2;
        agent.speed = 5;
        agent.isStopped = true;
        inThirdStage = true;
        projectileSpeed *= 1.25f;
    }

    //hier shield damage 
    public void TakeDamageToShield()
    {
        shieldHealh--;
        if(shieldHealh == 0)
        {
            canBeDamaged = true;
            isChangingStance = true;
            agent.SetDestination(transform.position);
            aura.gameObject.SetActive(false);
            bzoop.gameObject.SetActive(false);
            if (inSecondStage)
            {
                if (!inThirdStage)
                {
                    ToThirdStage();
                }
                else
                {
                    isChangingStance = false;
                }
            }
            else
            {
                ToSecondStage();
            }
        }
    }
    public void MayMoveAgain()
    {
        agent.isStopped = false;
    }
    public void RandomFunctie()
    {
        canBeDamaged = false;
        isChangingStance = false;
        bzoop.gameObject.SetActive(true);
        aura.gameObject.SetActive(true);
    }
}
