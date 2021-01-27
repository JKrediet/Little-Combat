using System.Collections;
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
        if (Input.GetButton("Fire1"))
        {
            transform.forward = new Vector3(cam.forward.x, transform.forward.y, cam.forward.z);

            // Shoot the laser
            ShootLaser();
        }else
        {
            StopLaser();
        }
    }
    private void FixedUpdate()
    {
        ObjectRefDistanceCheck();
    }

    private void ShootLaser()
    {
        // Rotate the player towards the laser
        //transform.forward = new Vector3(cam.forward.x, transform.forward.y, cam.forward.z);
        //funtion in playermovement

        Ray ray = new Ray(shootPoint.position, shootPoint.forward);
        RaycastHit _hit;

        if(Physics.Raycast(ray, out _hit, lineLength))
        {
            lineRen.SetPosition(0, shootPoint.position);
            lineRen.SetPosition(1, _hit.point);

            if (_hit.transform.tag == "Reflective")
            {
                _hit.transform.GetComponent<Reflective>().OnReflection(_hit.point, transform.forward, _hit.normal);
            }
            else
            {
                Interaction tempInt = _hit.transform.GetComponent<Interaction>();
                if (tempInt)
                {
                    tempInt.OnInteraction();
                }
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
            lineRen.SetPosition(0, shootPoint.position);
            lineRen.SetPosition(1, transform.position + shootPoint.forward * lineLength);
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
