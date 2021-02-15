using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    public float attackDamage;
    public float attackRange = 2, attackCooldown = 1;
    private float targetDistance, nextAttack;
    
    //animation purpose
    protected bool playerInRange, isAttacking, idle;
    protected Animator anim;

    private NavMeshAgent agent;
    private GameObject player;
    private Vector3 faceThisDirection;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerMovement>().gameObject;
        if(GetComponent<Animator>())
        {
            anim = GetComponent<Animator>();
        }

        CheckDistance();
    }
    private void Update()
    {
        Movement();
        CheckDistance();    
        AnimationThings();
    }

    private void LateUpdate()
    {
    }

    private void CheckDistance()
    {
        targetDistance = Vector3.Distance(transform.position, player.transform.position);
    }

    private void Movement()
    {
        if (!isAttacking)
        {
            if (targetDistance <= attackRange)
            {
                playerInRange = true;
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

                playerInRange = false;
                if (!idle)
                {
                    agent.SetDestination(player.transform.position - transform.forward * (attackRange / 2));
                }
            }
        }
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
