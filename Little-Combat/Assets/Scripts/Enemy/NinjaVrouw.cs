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

    public float playerDetectionRange, retreatRange;
    private float targetDistance;
    private bool playerInRange, isAttacking, retreat, dash;

    public Slider healthSlider;
    public TMP_Text healthText;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerMovement>().transform;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        CheckDistance();
        Movement();
    }
    private void Movement()
    {
        if(playerInRange)
        {
            if(!dash)
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
            else
            {
                
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
        Retreat();
    }
    private void Retreat()
    {
        retreat = true;
        agent.speed = 7;
        agent.stoppingDistance = 0;
        anim.SetInteger("State", 3);
    }
    public void StopRetreat()
    {
        retreat = false;
        agent.stoppingDistance = 5;
        agent.speed = 3.5f;
        anim.SetInteger("State", 0);
    }
}
