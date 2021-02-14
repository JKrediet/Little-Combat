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
    protected bool playerInRange, isAttacking;
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
    }
    private void Update()
    {
        CheckDistance();
        Movement();
        AnimationThings();
    }
    private void CheckDistance()
    {
        targetDistance = Vector3.Distance(transform.position, player.transform.position);
    }
    private void Movement()
    {
        if (!isAttacking)
        {
            if (attackRange >= targetDistance)
            {
                playerInRange = true;
                if (Time.time >= nextAttack)
                {
                    nextAttack = Time.time + attackCooldown;
                    Attack();
                }
            }
            else
            {
                playerInRange = false;
                agent.SetDestination(player.transform.position - transform.forward * (attackRange / 2));
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
