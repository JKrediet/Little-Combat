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
    }
    public void AttackHitbox()
    {
        //actual attack
        Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward, 1);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                if (collider.GetComponent<PlayerHealth>())
                {
                    collider.GetComponent<PlayerHealth>().GiveDamage(attackDamage);
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + transform.forward, 0.5f);
    }
}
