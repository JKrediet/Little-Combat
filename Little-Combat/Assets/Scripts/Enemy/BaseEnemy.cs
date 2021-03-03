using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class BaseEnemy : MonoBehaviour
{
    public string bossName;

    public float maxHealth;

    public Slider healthSlider;
    public TMP_Text healthText;

    public bool canBeDamaged = false;

    public float attackDamage;
    public float attackRange = 2, attackCooldown = 1, playerDetectionRange = 100, rangedAttackRange = 10;
    public GameObject hitParticle;
    
    protected float targetDistance, nextAttack;
    
    //animation purposes
    protected bool playerInRange, isAttacking, idle;
    protected Animator anim;

    protected NavMeshAgent agent;
    protected GameObject player;
    private Vector3 faceThisDirection;
    protected float health;

    //open gate
    protected bool bossDead;
    public Transform moveObject, originHere, goHere;

    protected virtual void Start()
    {
        healthSlider.maxValue = maxHealth;

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
        if(healthSlider != null)
        {
            healthSlider.value = health;
            
        }

        if(healthText != null)
        {
            healthText.text = bossName + " | " + health.ToString() + "/" + maxHealth.ToString();
        }

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
            GetComponent<Collider>().enabled = false;
            moveObject.position = new Vector3(originHere.position.x, Mathf.Lerp(moveObject.position.y, goHere.position.y, 0.1f), originHere.position.z);
        }
    }

    private void CheckDistance()
    {
        targetDistance = Vector3.Distance(transform.position, player.transform.position);
        if(targetDistance < playerDetectionRange)
        {
            playerInRange = true;

            if(healthSlider != null)
            {
                healthSlider.gameObject.SetActive(true);
                
            }

            if(healthText != null)
            {
                healthText.gameObject.SetActive(true);
            }
        }
    }

    public void GiveDamage(float _damageTaken)
    {
        if (canBeDamaged)
        {
            health = Mathf.Clamp(health - _damageTaken, 0, maxHealth);
        }
    }

    protected virtual void Movement()
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
                    Vector3 direction = (player.transform.position - transform.position).normalized;
                    transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
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
        //boss 2
    }

    //comes from animation
    public virtual void DoneAttacking()
    {
        isAttacking = false;
    }
}
