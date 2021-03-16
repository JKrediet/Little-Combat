using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss1 : BaseEnemy
{
    public AudioClip footOne;
    public AudioClip footTwo;

    public AudioClip attackSound;

    public AudioSource source;

    public AudioSource doorSource;

    protected override void Attack()
    {
        base.Attack();
    }
    protected override void AnimationThings()
    {
        anim.SetBool("_attack", isAttacking);
        anim.SetBool("isIdle", idle);
        anim.SetBool("startFlex", playerInRange);

        if (health <= 0f)
        {
            if(doorSource != null)
            {
                doorSource.Play();
            }

            agent.isStopped = true;
            anim.SetBool("isDead", true);
            bossDead = true;
            PlayerPrefs.SetInt("tutorial_boss1", 1);

            if (healthSlider != null)
            {
                healthSlider.gameObject.SetActive(false);
                
            }

            if(healthText != null)
            {
                healthText.gameObject.SetActive(false);
            }
        }
    }

    public void FootOne()
    {
        source.Stop();
        source.clip = footOne;
        source.Play();
    }

    public void FootTwo()
    {
        source.Stop();
        source.clip = footTwo;
        source.Play();
    }

    public void AttackSound()
    {
        source.Stop();
        source.clip = attackSound;
        source.Play();
    }

    public void AttackHitbox()
    {
        //actual attack
        Collider[] colliders = Physics.OverlapBox(transform.position + transform.forward * 2, new Vector3(2, 15, 2));
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
}
