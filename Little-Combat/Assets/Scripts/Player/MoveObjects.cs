﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjects : MonoBehaviour
{
    public float lineLength, collisionOffset = 0.2f;
    //pushable object reference
    public Transform objectDump, objectLocation;
    private Transform pushRef;
    private CharacterController controller;
    public bool putObjectInPos, isHolding;
    private Vector3 pos;
    public LayerMask maskuuuuu;

    //playercollision
    //privates
    private Vector2 rotation;
    private Vector3 defaultPos, directionNormalized, collisionPos;
    private float defaultDistance;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        directionNormalized = defaultPos.normalized;
        defaultDistance = Vector3.Distance(defaultPos, Vector3.zero);
    }
    void FixedUpdate()
    {
        CollisionRaycast();
        if (!isHolding)
        {
            if (Input.GetButton("Fire2"))
            {
                MoveObject();
            }
            else
            {
                isHolding = true;
                StopMovingObjects();
            }
        }
        if (pushRef)
        {
            float distance = Vector3.Distance(pushRef.position, transform.position + transform.forward);
            if (distance > 1.1f)
            {
                isHolding = true;
                StopMovingObjects();
            }
        }
        CheckPlayerCollision(); // shhhhhh
        //wall check for pushing/holding objects
        if (pushRef != null)
        {
            pushRef.rotation = transform.rotation;
        }
    }

    private void MoveObject()
    {
        // Save raycast data here
        RaycastHit _hit_Object;

        // Shoot a raycast and check if it hit anything
        if (Physics.Raycast(transform.position, transform.forward, out _hit_Object, lineLength))
        {
            // if hits pushalbe object
            if (_hit_Object.transform.tag == "Pickup")
            {
                //StopMovingObjects();
                Unparent();

                GetComponent<PlayerMovement>().isHoldingPickup = true;
                //make player parent of object
                pushRef = _hit_Object.transform;
                pushRef.SetParent(transform);
                //pushRef.gameObject.layer = 11;

                //set object to position in front of player
                if (!putObjectInPos)
                {
                    pushRef.localPosition = new Vector3(0, 0, 2);
                    putObjectInPos = true;
                    pushRef.GetComponent<Rigidbody>().useGravity = !putObjectInPos;
                }
                pushRef.GetComponent<Rigidbody>().velocity = Vector3.zero;
                pushRef.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            }
            else if (_hit_Object.transform.tag == "Push")
            {
                Unparent();

                //StopMovingObjects();

                //make player know it is pushing something
                GetComponent<PlayerMovement>().status_Push = true;

                //make player parent of object
                pushRef = _hit_Object.transform;
                pushRef.SetParent(transform);
                //pushRef.gameObject.layer = 11;

                //set object to position in front of player
                if (!putObjectInPos)
                {
                    pushRef.localPosition = new Vector3(0, 0, 2);
                    putObjectInPos = true;
                    pushRef.GetComponent<Rigidbody>().useGravity = !putObjectInPos;
                }
            }
            else if (_hit_Object.transform.tag == "Laser")
            {
                Unparent();

                //make player parent of object
                pushRef = _hit_Object.transform;
                pushRef.SetParent(transform);
                pushRef.gameObject.layer = 11;
                GetComponent<PlayerMovement>().isHoldingLaser = true;

                //set object to position in front of player
                if (!putObjectInPos)
                {
                    pushRef.localPosition = new Vector3(0, 0, 2);
                    putObjectInPos = true;
                }
            }
        }
        else
        {
            GetComponent<PlayerMovement>().status_Push = false;

        }
    }
    private void StopMovingObjects()
    {
        if (pushRef)
        {
            if (pushRef.transform.tag != "Laser")
            {
                // gravity weer aan
                pushRef.GetComponent<Rigidbody>().useGravity = true;
            }
            //remove object as player child
            pushRef.SetParent(objectDump);
            //pushRef.gameObject.layer = 0;
            GetComponent<PlayerMovement>().status_Push = false;
            GetComponent<PlayerMovement>().isHoldingLaser = false;
            GetComponent<PlayerMovement>().isHoldingPickup = false;
            putObjectInPos = false;
            pushRef = null; // deze helemaal onderaan
        }
        isHolding = false;
    }

    private void Unparent()
    {
        if(pushRef != null)
        {
            GetComponent<PlayerMovement>().status_Push = false;
            pushRef.SetParent(objectDump);
        }
    }

    private void CheckPlayerCollision()
    {
        if (collisionPos != Vector3.zero)
        {
            Vector3 currentPos = transform.position - transform.forward;

            transform.localPosition = Vector3.Lerp(transform.position, currentPos, Time.deltaTime * 15f);
        }
    }
    private void CollisionRaycast()
    {
        RaycastHit _hit;
        if(Physics.Raycast(transform.position, transform.forward, out _hit, 1, maskuuuuu))
        {
            StopMovingObjects();
            collisionPos = _hit.transform.position;
        }
        else
        {
            collisionPos = Vector3.zero; ;
        }
    }
}
