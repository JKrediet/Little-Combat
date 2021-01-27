using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjects : MonoBehaviour
{

    public float lineLength;
    //pushable object reference
    public Transform objectDump;
    private Transform pushRef;

    void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            MoveObject();
        }
        else
        {
            StopMovingObjects();
        }

        //wall check for pushing/holding objects
        if (pushRef != null)
        {
            ObjectWallCheck();
            pushRef.rotation = transform.rotation;
        }
    }
    //werkt nog niet helemaal
    private void FixedUpdate()
    {
        ObjectRefDistanceCheck();
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

                //set object to position in front of player
                pushRef.localPosition = new Vector3(0, 0, 2);
            }
            if (_hit_Object.transform.tag == "Push")
            {
                //make player know it is pushing something
                GetComponent<PlayerMovement>().status_Push = true;

                //make player parent of object
                pushRef = _hit_Object.transform;
                pushRef.SetParent(transform);

                //set object to position in front of player
                pushRef.localPosition = new Vector3(0, 0, 2);
            }
        }
    }
    private void StopMovingObjects()
    {
        if (pushRef != null)
        {
            //remove object as player child
            pushRef.SetParent(objectDump);
            GetComponent<PlayerMovement>().status_Push = false;
            pushRef = null;
        }
    }
    //check if youre pushing it against a wall
    private void ObjectWallCheck()
    {
        // Save raycast data here
        RaycastHit _wall_forward;
        RaycastHit _wall_right;
        RaycastHit _wall_left;

        // Shoot a raycast and check if it hits forward of hold object
        if (Physics.Raycast(pushRef.position, transform.forward, out _wall_forward, 1))
        {
            GetComponent<PlayerMovement>().push_forward = true;
        }
        else
        {
            GetComponent<PlayerMovement>().push_forward = false;
        }
        // Shoot a raycast and check if it hits right of hold object
        if (Physics.Raycast(pushRef.position, transform.right, out _wall_right, 1))
        {
            GetComponent<PlayerMovement>().push_right = true;
        }
        else
        {
            GetComponent<PlayerMovement>().push_right = false;
        }
        // Shoot a raycast and check if it hits left of hold object
        if (Physics.Raycast(pushRef.position, -transform.right, out _wall_left, 1))
        {
            GetComponent<PlayerMovement>().push_left = true;
        }
        else
        {
            GetComponent<PlayerMovement>().push_left = false;
        }
    }

    //if object to far from holdlocation, let go of it
    private void ObjectRefDistanceCheck()
    {
        // werkt gek met push kijk later na <--
        //if (pushRef != null)
        //{
        //    float distance = Vector3.Distance(transform.localPosition + new Vector3(0,0,2), pushRef.position);
        //    if (distance > 2)
        //    {
        //        StopLaser();
        //    }
        //}
    }
}
