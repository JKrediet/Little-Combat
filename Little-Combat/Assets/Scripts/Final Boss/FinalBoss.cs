using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    public List<int> crystals;
    public List<Transform> totems, TotemPos, activeTotem, magicAttackPositions;

    public Animator anim;
    public Rigidbody fireBall;
    public bool nextAttackNoTotems, playerInMeleeRange, mayAct;
    public int state, stage;
    public Transform attackPos, shieldCheck, magicAttack, meteorIndecator, crystal0, crystal1, crystal2, crystal3;
    public LayerMask shield;
    public float attackDamage, slomoTime = 2, cooldown;
    private float nextAttack;
    private Transform player;

    //arms and masks
    public GameObject arms, cylinders, happyMask, angryMask;
    public bool mask1, mask2, rotate;

    //dissolve
    private bool dissolveOn, dissolveOff;
    private Material mat;
    public float speed = 1;
    private float amount;

    private void Awake()
    {
        for(int i = 0; i < 4; i++)
        {
            crystals.Add(0);
        }
        foreach(Transform child in magicAttack)
        {
            magicAttackPositions.Add(child);
        }
        //anim.speed = 0;
    }

    private void Update()
    {
        if(mayAct)
        {
            if (state == 0)
            {
                if (Time.time > nextAttack)
                {
                    nextAttack = Time.time + cooldown;
                    Attack();
                }
            }
            if (rotate)
            {
                if (mask1)
                {
                    if(!mask2)
                    {
                        happyMask.transform.Rotate(new Vector3(0, -90, 0) * Time.deltaTime, Space.Self);
                        Invoke("StopRotating", 1);
                    }
                }
                if (mask2)
                {
                    angryMask.transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime, Space.Self);
                    Invoke("StopRotating", 1);
                }
            }
        }
        Dissolve();
    }
    #region dissolve
    public void Dissolve()
    {
        if (dissolveOn)
        {
            if (amount < 1)
            {
                amount += speed * Time.deltaTime;
                mat.SetFloat("_CutoffHeight", amount);
            }
            else
            {
                dissolveOn = false;
            }
        }
        if (dissolveOff)
        {
            if (amount > -1)
            {
                amount -= speed * Time.deltaTime;
                mat.SetFloat("_CutoffHeight", amount);
            }
            else
            {
                dissolveOff = false;
            }
        }
    }
    public void DissolveOn()
    {
        dissolveOn = true;
    }
    public void DissolveOff()
    {
        dissolveOff = true;
    }
    #endregion
    public void StopRotating()
    {
        rotate = false;
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
    //gets called from crystals
    public void ToNextStage()
    {
        anim.speed = 1;
        anim.SetBool("ChangingState", true);
        if (stage == 0)
        {
            rotate = true;
            mask1 = true;
            Invoke("CrystalsOn", 2);
        }
        else if (stage == 1)
        {
            Destroy(happyMask);
            rotate = true;
            mask2 = true;
            Invoke("CrystalsOn", 2);
        }
        else if (stage == 2)
        {
            Destroy(angryMask);
            Invoke("CrystalsOn", 2);
        }
        else if (stage == 3)
        {
            Destroy(gameObject);
        }
        stage++;
    }
    public void CrystalsOn()
    {
        crystal0.GetComponent<Crystals>().RestoreCrystal();
        crystal1.GetComponent<Crystals>().RestoreCrystal();
        crystal2.GetComponent<Crystals>().RestoreCrystal();
        crystal3.GetComponent<Crystals>().RestoreCrystal();
    }
    public void IdleAgain()
    {
        anim.SetBool("ChangingState", false);
    }

    public void Attack()
    {
        if(playerInMeleeRange)
        {
            //rolls between arm attacks
            state = Random.Range(1, 3);
        }
        else if (nextAttackNoTotems || activeTotem.Count > 0)
        {
            state = 4;
        }
        else
        {
            //rolls between all attacks
            state = 3;
        }
        nextAttackNoTotems = false;
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
            nextAttackNoTotems = true;
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
                if (collider.gameObject.CompareTag("Player"))
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
        if (Physics.Linecast(shieldCheck.position, target, out hit, shield))
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
        //foreach (Transform totem in TotemPos)
        //{

        //}
        int random = Random.Range(0, totems.Count);
        Instantiate(totems[random], TotemPos[Random.Range(0, TotemPos.Count)].position, Quaternion.identity);
    }
    public IEnumerator SpawnMagic()
    {
        for (int i = 0; i < 20; i++)
        {
            SpawnFireBall();
            SpawnFireBall();
            yield return new WaitForSeconds(0.5f);
        }
    }
    public void SpawnFireBall()
    {
        int roll = Random.Range(0, 11);
        Rigidbody fire = Instantiate(fireBall, magicAttackPositions[roll].position, transform.rotation);
        fire.GetComponent<FireBall>().originObject = magicAttackPositions[roll];
        fire.velocity = fire.transform.forward * 20;
    }
    public void MagicAttack()
    {
        int roll = Random.Range(0, 2);
        if(roll == 0)
        {
            StartCoroutine(SpawnMagic());
        }
        else if(roll == 1)
        {
            StartCoroutine(SummonMeteors());
        }

    }
    private void OnTriggerEnter(Collider _objectsInRange)
    {
        if(_objectsInRange.CompareTag("Player"))
        {
            playerInMeleeRange = true;
        }
    }
    private void OnTriggerExit(Collider _objectsInRange)
    {
        if (_objectsInRange.CompareTag("Player"))
        {
            playerInMeleeRange = false;
        }
    }
    private IEnumerator SummonMeteors()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        int roll = Random.Range(3, 6);
        for (int i = 0; i < roll; i++)
        {
            Instantiate(meteorIndecator, player.position - player.up, Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
    }
}
