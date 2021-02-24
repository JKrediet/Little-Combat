using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    public float maxHealth;

    public bool canBeDamaged = false;

    public float attackDamage;
    public float attackRange = 2, attackCooldown = 1, playerDetectionRange = 100;
    private float targetDistance, nextAttack;
    
    //animation purposes
    protected bool playerInRange, isAttacking, idle;
    protected Animator anim;

    protected NavMeshAgent agent;
    private GameObject player;
    private Vector3 faceThisDirection;
    protected float health;

    public List<GameObject> bodyparts;

    //open gate
    protected bool bossDead;
    public Transform moveObject, originHere, goHere;

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
        GetComponent<Collider>().enabled = canBeDamaged;

        if (Input.GetKeyDown("k"))
        {
            health = 0f;
        }

        Movement();
        CheckDistance();    
        AnimationThings();

        if(bossDead)
        {
            moveObject.position = new Vector3(originHere.position.x, Mathf.Lerp(moveObject.position.y, goHere.position.y, 0.1f), originHere.position.z);
        }
    }

    private void CheckDistance()
    {
        targetDistance = Vector3.Distance(transform.position, player.transform.position);
        if(targetDistance < playerDetectionRange)
        {
            playerInRange = true;
        }
    }

    public void GiveDamage(float _damageTaken)
    {
        if (canBeDamaged)
        {
            health = Mathf.Clamp(health - _damageTaken, 0, maxHealth);
            if (health == 0)
            {
                if (bodyparts.Count > 0)
                {
                    for(int i = 0; i < bodyparts.Count; i++)
                    {
                        bodyparts[i].GetComponent<Dissolve>().ActivateDissolve();
                    }
                }
                //if(anim != null)
                //{
                //    anim.enabled = false;
                //}
            }
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
