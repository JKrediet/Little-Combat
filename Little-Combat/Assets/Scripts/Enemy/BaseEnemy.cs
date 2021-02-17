using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    public float maxHealth;

    public float attackDamage;
    public float attackRange = 2, attackCooldown = 1, playerDetectionRange = 100;
    private float targetDistance, nextAttack;
    
    //animation purpose
    protected bool playerInRange, isAttacking, idle;
    protected Animator anim;

    protected NavMeshAgent agent;
    private GameObject player;
    private Vector3 faceThisDirection;
    protected float health;

    private void Start()
    {
        health = maxHealth;

        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerMovement>().gameObject;
        if(GetComponent<Animator>())
        {
            anim = GetComponent<Animator>();
        }
        agent.isStopped = true;

        CheckDistance();
    }

    private void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            health = 0f;
        }

        Movement();
        CheckDistance();    
        AnimationThings();
    }

    private void CheckDistance()
    {
        targetDistance = Vector3.Distance(transform.position, player.transform.position);
        if(targetDistance < playerDetectionRange)
        {
            playerInRange = true;
        }
    }

    private void Movement()
    {
        if (playerInRange)
        {
            if (!isAttacking)
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
        }
    }
    public void StartMoving()
    {
        agent.isStopped = false;
    }
    protected virtual void Attack()
    {
        isAttacking = true;
        //does attack!
    }
    protected virtual void AnimationThings()
    {
        //boss 1
    }

    //comes from animation
    public void DoneAttacking()
    {
        isAttacking = false;
    }
}
