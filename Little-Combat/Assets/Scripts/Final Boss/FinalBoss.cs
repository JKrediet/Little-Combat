using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    public List<int> crystals;

    public Animator anim;
    public bool playerInRange;
    public int state;
    public Transform attackPos;
    public LayerMask shield;
    public float attackDamage, waitTime = 5;

    private void Awake()
    {
        for(int i = 0; i < 4; i++)
        {
            crystals.Add(0);
        }
    }

    private void Update()
    {
        if(playerInRange)
        {
            if(state == 0)
            {
                playerInRange = false;
                Attack();
            }
        }
    }

    public void Attack()
    {
        state = 1;
        anim.SetInteger("State", state);
    }
    public void DamageHitBox()
    {
        //actual attack
        Collider[] colliders = Physics.OverlapSphere(attackPos.position, transform.lossyScale.z / 2);
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
    public void StopAttack()
    {
        state = 0;
        anim.SetInteger("State", state);
    }
    public void Pauze()
    {
        anim.speed = 0.01f;
        Invoke("Unpause", waitTime);
    }
    public void Unpause()
    {
        anim.speed = 1;
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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(attackPos.position, transform.lossyScale.z / 2);
    }
}
