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
                rb.velocity = Vector3.zero;
                transform.localPosition -= transform.forward * 0.5f;
                transform.SetParent(other.transform);
                transform.rotation = other.transform.rotation;
                isOnShield = true;
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
        if(Physics.Linecast(transform.position, FindObjectOfType<Boss2>().transform.position, shield))
        {
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
                rb.velocity = transform.forward * 20;
            }
        }
    }
}
