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

    }
    public void StopAttack()
    {
        state = 0;
        anim.SetInteger("State", state);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(attackPos.position, transform.lossyScale.z / 2);
    }
}
