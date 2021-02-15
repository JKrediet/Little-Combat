using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss1 : BaseEnemy
{   
    protected override void Attack()
    {
        base.Attack();
    }
    protected override void AnimationThings()
    {
        anim.SetBool("_attack", isAttacking);
        anim.SetBool("isIdle", idle);
    }

    public void AttackHitbox()
    {
        //actual attack
        Collider[] colliders = Physics.OverlapBox(transform.position + transform.forward * 2, new Vector3(2, 15, 2));
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                //Debug.Log(collider.transform.name);

                if (collider.GetComponent<PlayerHealth>())
                {
                    collider.GetComponent<PlayerHealth>().GiveDamage(attackDamage);
                }
                else if(collider.GetComponent<ObjectHealth>())
                {
                    collider.GetComponent<ObjectHealth>().DoDamage();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position + transform.forward * 2, new Vector3(2, 15, 2));
    }
}
