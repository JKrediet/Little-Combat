using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public GameObject boem;
    private Rigidbody rb;
    private bool isOnShield;
    public LayerMask shield;

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
        if(isOnShield)
        {
            if(other.tag == "Boss")
            {
                other.GetComponent<BaseEnemy>().GiveDamage(10);
                Explode();
            }
        }
        if(other.tag == "Charge")
        {
            other.GetComponent<Charges>().TurnOn();
            Explode();
        }
    }

    private bool CheckForShield()
    {
        RaycastHit hit;
        if(Physics.Linecast(transform.position, FindObjectOfType<Boss2>().transform.position, out hit, shield))
        {
            Transform shield = hit.transform.GetComponent<PlayerMovement>().shield;
            FindObjectOfType<PlayerMovement>().FireBallHit();
            rb.velocity = Vector3.zero;
            transform.SetParent(shield);
            transform.rotation = shield.transform.rotation;
            transform.position = shield.transform.position;
            transform.localPosition += transform.forward * 0.5f;
            isOnShield = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Explode()
    {
        GameObject UwU = Instantiate(boem, transform.position, transform.rotation);
        Destroy(UwU, 2);
        Destroy(gameObject);
    }
    private void Update()
    {
        if(isOnShield)
        {
            if(Input.GetButtonDown("Shield") || Input.GetButtonDown("Fire1"))
            {
                transform.SetParent(null);
                rb.velocity = -transform.up * 20;
            }
        }
    }
}
