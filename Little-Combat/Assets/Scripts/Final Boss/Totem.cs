using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{
    public Transform player;
    public Rigidbody fireBall;
    public float projectileSpeed;
    private float nextAttack, cooldown = 2;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    private void Update()
    {
        Vector3 pos = (player.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(new Vector3(pos.x, 0, pos.z));
        Fire();
    }
    public void Fire()
    {
        if(Time.time > nextAttack)
        {
            nextAttack = Time.time + cooldown;
            Rigidbody fire = Instantiate(fireBall, transform.position + transform.up, transform.rotation);
            fire.velocity = fire.transform.forward * projectileSpeed;
        }
    }
}
