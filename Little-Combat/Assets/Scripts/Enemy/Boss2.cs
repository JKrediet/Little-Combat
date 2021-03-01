using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss2 : BaseEnemy
{
    public LayerMask playerMask;
    public int currentAttack;
    public Rigidbody fireBall;
    public float projectileSpeed;

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
    protected override void Movement()
    {
        if (playerInRange)
        {
            if (!isAttacking)
            {
                if(currentAttack == 0)
                {
                    agent.isStopped = false;
                    currentAttack = Random.Range(1, 3);
                }
                if (currentAttack == 1)
                {
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
                            agent.SetDestination(player.transform.position - transform.forward * (attackRange / 2));
                        }
                    }
                }
                if(currentAttack == 2)
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
                transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0 , direction.z));
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
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position + transform.forward, new Vector3(1, 3, 1));
    }
}
