using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public GameObject boem;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().GiveDamage(1);
            GameObject UwU = Instantiate(boem, transform.position,transform.rotation);
            Destroy(UwU, 2);
            Destroy(gameObject);
        }
    }
}
