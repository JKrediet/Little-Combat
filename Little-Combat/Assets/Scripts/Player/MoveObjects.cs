using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjects : MonoBehaviour
{

    public float lineLength;
    //pushable object reference
    public Transform objectDump, objectLocation;
    private Transform pushRef;
    private CharacterController controller;
    public bool putObjectInPos, isHolding;
    private Vector3 pos;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    void FixedUpdate()
    {
        if (!isHolding)
        {
            if (Input.GetButton("Fire2"))
            {
                MoveObject();
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
        if (Input.GetButtonUp("Fire2"))
        {
            StopMovingObjects();
        }

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
            if (_hit_Object.transform.tag == "Push")
            {
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
            if (_hit_Object.transform.tag == "Laser")
            {
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
            isHolding = true;
            StopMovingObjects();
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
            pushRef.gameObject.layer = 0;
            GetComponent<PlayerMovement>().status_Push = false;
            GetComponent<PlayerMovement>().isHoldingLaser = false;
            putObjectInPos = false;
            pushRef = null; // deze helemaal onderaan
        }
        isHolding = false;
    }
}
