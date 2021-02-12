using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    public float attackRange = 1, attackCooldown = 1;
    private float targetDistance, nextAttack;

    private NavMeshAgent target;
    private GameObject player;

    private void Start()
    {
        target = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }
    private void Update()
    {
        CheckDistance();
        Movement();
    }
    private void CheckDistance()
    {
        targetDistance = Vector3.Distance(transform.position, target.transform.position);
    }
    private void Movement()
    {
        if (attackRange >= targetDistance)
        {
            if(Time.time >= nextAttack)
            {
                nextAttack = Time.time + attackCooldown;
                Attack();
            }
        }
        else
        {
            target.SetDestination(player.transform.position);
        }
    }
    protected virtual void Attack()
    {
        //does attack!
        print("Slap");
    }
}
