using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class BaseEnemy : MonoBehaviour
{
    public GameObject healthBar;

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
    public LayerMask shield;

    protected virtual void Start()
    {
        if(healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
        }

        if (healthBar)
        {
            healthBar.SetActive(false);
        }

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

        if (healthText != null)
        {
            healthText.text = bossName + " | " + health.ToString() + "/" + maxHealth.ToString();
        }

        if (!bossDead)
        {
            Movement();
            CheckDistance();
            AnimationThings();
        }

        if(bossDead)
        {
            moveObject.position = new Vector3(originHere.position.x, Mathf.Lerp(moveObject.position.y, goHere.position.y, 0.1f), originHere.position.z);
        }
    }
    public void KillBoss()
    {
        health = 0;
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

            if (healthBar)
            {
                healthBar.SetActive(true);
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
        //  GetComponent<Collider>().enabled = canBeDamaged;
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
        if(idle)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)), 0.1f);
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
    protected bool CheckForShield(Vector3 target)
    {
        RaycastHit hit;
        if (Physics.Linecast(transform.position + transform.up, target, out hit, shield))
        {
            FindObjectOfType<PlayerMovement>().FireBallHit();
            return false;
        }
        else
        {
            return true;

        }
    }
}
