﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform shootPoint;
    public LineRenderer lineRen;

    public float lineLength;

    private Transform cam;

    //pushable object reference
    public Transform objectDump;
    private Transform pushRef;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
    }

    // Update is called every frame after update
    void LateUpdate()
    {
        // Check for left mouse button input;
        if (Input.GetButton("Fire1"))
        {
            StartLaser();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopLaser();
        }
    }
    private void FixedUpdate()
    {
        ObjectRefDistanceCheck();
    }

    private void StartLaser()
    {
        // Rotate the player towards the laser
        //transform.forward = new Vector3(cam.forward.x, transform.forward.y, cam.forward.z);
        //funtion in playermovement

        // Save raycast data here
        RaycastHit _hit;

        // Shoot a raycast and check if it hit anything
        if(Physics.Raycast(transform.position, transform.forward, out _hit, lineLength))
        {
            // If it hits anything

            // Show the line renderer
            lineRen.enabled = true;

            // Draw the line towards the point the raycast hits
            lineRen.SetPosition(0, shootPoint.position);
            lineRen.SetPosition(1, _hit.point);

            // TODO: trigger interactions
            Interaction tempInt = _hit.transform.GetComponent<Interaction>();
            if(tempInt != null)
            {
                tempInt.OnInteraction();
            }

            // if hits pushalbe object
            if(_hit.transform.tag == "Pickup")
            {
                //make player parent of object
                pushRef = _hit.transform;
                pushRef.SetParent(transform);

                //set object to position in front of player
                pushRef.localPosition = new Vector3(0, 0, 2);
            }
        }
        else
        {
            // If nothing is hit

            // Show the line renderer
            lineRen.enabled = true;

            // Draw the linerenderer over the level
            lineRen.SetPosition(0, shootPoint.position);
            lineRen.SetPosition(1, transform.position + transform.forward * lineLength);
        }
    }

    private void StopLaser()
    {
        lineRen.enabled = false;

        if(pushRef != null)
        {
            //remove object as player child
            pushRef.SetParent(objectDump);
            pushRef = null;
        }
    }

    private void ObjectRefDistanceCheck()
    {
        if (pushRef != null)
        {
            float distance = Vector3.Distance(transform.localPosition + new Vector3(0,0,2), pushRef.position);
            if (distance > 1)
            {
                StopLaser();
            }
        }
    }
}
