﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public GameObject boem;
    public LayerMask shield;
    public Transform originObject;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == originObject)
        {
            //other.GetComponent<MeteorPrefab>().DissolveOff();
            //actual attack
            
            Explode();
        }
    }
    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, transform.lossyScale.z / 2);
        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<PlayerHealth>())
            {
                collider.GetComponent<PlayerHealth>().GiveDamage(1);
            }
        }
        GameObject UwU = Instantiate(boem, transform.position, transform.rotation);
        Destroy(UwU, 2);
        Destroy(gameObject);
    }
    protected bool CheckForShield(Vector3 target)
    {
        RaycastHit hit;
        if (Physics.Linecast(transform.position, target, out hit, shield))
        {
            FindObjectOfType<PlayerMovement>().FireBallHit();
            return false;
        }
        else
        {
            return true;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 2.5f);
    }
}
