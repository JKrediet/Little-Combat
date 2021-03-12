using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class NinjaVrouw : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    private Transform player;
    //state: state0 = idle /state1 = run/ state2 = attack/ state3 = retreat

    public float playerDetectionRange, retreatRange, attackDamage, maxHealth;
    public bool canBeDamaged, bossDead;
    private float targetDistance, health;
    private bool playerInRange, isAttacking, retreat, dash, idle;

    public Slider healthSlider;
    public TMP_Text healthText;
    public LayerMask shield;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerMovement>().transform;
        anim = GetComponent<Animator>();

        health = maxHealth;
    }
    private void Update()
    {
        CheckDistance();
        Movement();

        if (health <= 0f)
        {
            agent.isStopped = true;
            anim.SetBool("isDead", true);
            bossDead = true;
            PlayerPrefs.SetInt("tutorial_boss1", 1);
        }
    }
    public void GiveDamage(float _damageTaken)
    {
        if (canBeDamaged)
        {
            health = Mathf.Clamp(health - _damageTaken, 0, maxHealth);
        }
    }
    private void Movement()
    {
        if (!bossDead)
        {
            if (playerInRange)
            {
                if (!idle)
                {
                    if (!dash)
                    {
                        if (!isAttacking)
                        {
                            if (!retreat)
                            {
                                agent.SetDestination(player.position);
                                agent.updateRotation = true;

                                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(player.position.x - transform.position.x, 0, player.position.z - transform.position.z)), 0.2f);
                                anim.SetInteger("State", 1);
                                if (targetDistance < 6)
                                {
                                    if (!retreat)
                                    {
                                        dash = true;
                                        Attack();
                                        Invoke("DashToPlayer", 0.5f);
                                    }
                                }
                            }
                            else
                            {
                                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(player.position.x - transform.position.x, 0, player.position.z - transform.position.z)), 0.2f);
                                agent.SetDestination(transform.position - transform.forward);
                                if (targetDistance > retreatRange)
                                {
                                    StopRetreat();
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (!isAttacking)
                {
                    agent.SetDestination(transform.position);
                    anim.SetInteger("State", 0);
                }
            }
        }
    }
    private void CheckDistance()
    {
        targetDistance = Vector3.Distance(transform.position, player.transform.position);
        if (targetDistance < playerDetectionRange)
        {
            playerInRange = true;
            if (healthSlider != null)
            {
                healthSlider.gameObject.SetActive(true);
            }
            if (healthText != null)
            {
                healthText.gameObject.SetActive(true);
            }
        }
        else
        {
            playerInRange = false;
        }
    }
    private void DashToPlayer()
    {
        agent.isStopped = true;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(player.position.x - transform.position.x, 0, player.position.z - transform.position.z)), 1);
        agent.SetDestination(player.position - transform.forward);
        transform.position = new Vector3(player.position.x - transform.forward.x, 0, player.position.z - transform.forward.z);
        agent.updateRotation = false;
    }
    private void Attack()
    {
        isAttacking = true;
        dash = false;
        anim.SetInteger("State", 2);
    }
    public void StopAttack()
    {
        isAttacking = false;
        anim.SetInteger("State", 0);
        agent.speed = 3.5f;
        agent.isStopped = false;
        float roll = Random.Range(0, 2);
        if (roll > 0)
        {
            Attack();
            Invoke("DashToPlayer", 0.5f);
        }
        else
        {
            Retreat();
        }
    }
    private void Retreat()
    {
        retreat = true;
        agent.stoppingDistance = 0;
        anim.SetInteger("State", 3);
        Invoke("StopRetreat", 2);
    }
    public void StopRetreat()
    {
        retreat = false;
        agent.stoppingDistance = 5;
        anim.SetInteger("State", 0);
        IdleToggle();
        Invoke("IdleToggle", 1);
    }
    private void IdleToggle()
    {
        idle = !idle;
    }
    public void DamageAttack()
    {
        //actual attack
        Collider[] colliders = Physics.OverlapBox(transform.position + transform.forward * 2, new Vector3(1, 2, 1));
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                if (CheckForShield(collider.transform.position))
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
    }
    private bool CheckForShield(Vector3 target)
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
