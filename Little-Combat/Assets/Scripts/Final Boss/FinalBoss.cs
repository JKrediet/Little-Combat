using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    public List<int> crystals;
    public List<Transform> totems, TotemPos, activeTotem;

    public Animator anim;
    public bool nextAttackNoAoe;
    public int state;
    public Transform attackPos;
    public LayerMask shield;
    public float attackDamage, slomoTime = 2, cooldown;
    private float nextAttack;

    private void Awake()
    {
        for(int i = 0; i < 4; i++)
        {
            crystals.Add(0);
        }
    }

    private void Update()
    {
        if(state == 0)
        {
            if(Time.time > nextAttack)
            {
                nextAttack = Time.time + cooldown;
                Attack();
            }
        }
    }
    //check crystal states
    public bool CheckCrystals(int _value, int _value2)
    {
        for (int i = _value; i < _value2; i++)
        {
            if(crystals[i] != 5)
            {
                return false;
            }
        }
        return true;
    }
    public void ToNextStage()
    {
        anim.speed = 1;
        anim.SetBool("ChangingState", true); //note: moet weer naar false ergens
    }

    public void Attack()
    {
        if (nextAttackNoAoe || activeTotem.Count > 0)
        {
            //rolls between arm attacks
            state = Random.Range(1, 3);
        }
        else
        {
            //rolls between all attacks
            state = Random.Range(1, 4);
        }
        nextAttackNoAoe = false;
        //check if left arm is broken
        if (CheckCrystals(0, 2) == true)
        {
            state = 1;
        }
        //check if right arm is broken
        if (CheckCrystals(2, 4) == true)
        {
            state = 2;
        }
        if(state == 3)
        {
            nextAttackNoAoe = true;
        }
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
                    if (collider.GetComponent<PlayerHealth>())
                    {
                        collider.GetComponent<PlayerHealth>().GiveDamage(attackDamage);
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
        Invoke("Unpause", slomoTime);
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
    public void AttackTotem()
    {
        foreach (Transform totem in TotemPos)
        {
            int random = Random.Range(0,totems.Count);
            Instantiate(totems[random], totem.position, Quaternion.identity);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(attackPos.position, transform.lossyScale.z / 2);
    }
}
