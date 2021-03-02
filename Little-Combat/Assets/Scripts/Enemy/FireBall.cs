using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public GameObject boem;
    private Rigidbody rb;
    private bool isOnShield;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(!FindObjectOfType<PlayerMovement>().shieldMoving)
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
                GameObject UwU = Instantiate(boem, transform.position, transform.rotation);
                Destroy(UwU, 2);
                Destroy(gameObject);
            }
        }
    }
    private void Update()
    {
        if(isOnShield)
        {
            if(Input.GetButtonDown("Shield") || Input.GetButtonDown("Fire1"))
            {
                isOnShield = false;
                transform.SetParent(null);
                rb.velocity = transform.forward * 20;
            }
        }
    }
}
