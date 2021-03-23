using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{
    public Transform player, shootPos;
    public Rigidbody fireBall;
    public float projectileSpeed;
    private float nextAttack, cooldown = 2;
    private bool moveUp;
    private Vector3 origin, moveTo;

    public int health;

    private void Start()
    {
        origin = transform.position - transform.up * 2;
        moveTo = transform.position + transform.up;
        player = FindObjectOfType<PlayerMovement>().transform;
        cooldown = Random.Range(100, 301) * 0.01f;
        moveUp = true;
        FindObjectOfType<FinalBoss>().activeTotem.Add(gameObject.transform);
        Invoke("RemoveTotem", Random.Range(5, 10));
    }
    private void RemoveTotem()
    {
        moveUp = false;
        Destroy(gameObject, 1);
    }
    private void OnDestroy()
    {
        FindObjectOfType<FinalBoss>().activeTotem.Remove(gameObject.transform);
    }

    private void Update()
    {
        if(moveUp)
        {
            transform.position = Vector3.Lerp(transform.position, moveTo, 0.1f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, origin, 0.1f);
        }

        if(transform.position == moveTo)
        {
            Vector3 pos = (player.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(new Vector3(pos.x, 0, pos.z));
            Fire();
        }
    }
    public void Fire()
    {
        if(Time.time > nextAttack)
        {
            nextAttack = Time.time + cooldown;
            Rigidbody fire = Instantiate(fireBall, shootPos.position, transform.rotation);
            fire.GetComponent<FireBall>().originObject = transform;
            fire.velocity = fire.transform.forward * projectileSpeed;
        }
    }
}
