using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public GameObject boem;
    private Rigidbody rb;
    private bool isOnShield;
    public LayerMask shield;

    public Transform originObject;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(CheckForShield() == true)
            {
                //ooga verplaatst
            }
            else
            {
                other.GetComponent<PlayerHealth>().GiveDamage(1);
                Explode();
            }
        }
        else if(isOnShield)
        {
            if(other.tag == "Boss")
            {
                other.GetComponent<BaseEnemy>().GiveDamage(5);
                Explode();
            }
        }
        else if(other.tag == "Charge")
        {
            other.GetComponent<Charges>().TurnOn();
            Explode();
        }
        else if(other == originObject)
        {

        }
        else
        {
            //Explode();
        }
    }

    private bool CheckForShield()
    {
        //hiernatu
        RaycastHit hit;
        if (Physics.Linecast(transform.position, originObject.position, out hit, shield))
        {
            Transform shield = hit.transform.GetComponent<PlayerMovement>().shield;
            FindObjectOfType<PlayerMovement>().fireballs.Add(transform);
            FindObjectOfType<PlayerMovement>().FireBallHit();
            rb.velocity = Vector3.zero;
            transform.SetParent(shield);
            transform.rotation = shield.transform.rotation;
            transform.position = FindObjectOfType<PlayerMovement>().fireBallpos.position;
            transform.GetComponent<Collider>().enabled = false;
            isOnShield = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Explode()
    {
        if(originObject.GetComponent<Totem>())
        {
            originObject.GetComponent<Totem>().fireballs.Remove(gameObject.transform);
        }
        GameObject UwU = Instantiate(boem, transform.position, transform.rotation);
        Destroy(UwU, 2);
        Destroy(gameObject);
    }
    private void Update()
    {
        if(isOnShield)
        {
            if(Input.GetButtonDown("Shield") || Input.GetButtonDown("Fire1") || Input.GetButtonDown("Aim"))
            {
                ShootBall();
            }
        }
    }
    public void ShootBall()
    {
        transform.SetParent(null);
        transform.GetComponent<Collider>().enabled = true;
        rb.velocity = -transform.forward * 20;
    }
}
