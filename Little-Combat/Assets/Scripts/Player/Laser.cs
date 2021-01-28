﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform shootPoint;
    public LineRenderer lineRen;

    public float lineLength;

    private Transform cam;

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
            Reflective[] array = FindObjectsOfType<Reflective>();
            for (int i = 0; i < array.Length; i++)
            {
                array[i].OnEndReflection();
            }
        }
    }

    private void ShootLaser()
    {
        lineRen.enabled = true;

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
            else if (_hit.transform.tag == "Interactive")
            {

                Interaction tempInt = _hit.transform.GetComponent<Interaction>();
                if (tempInt)
                {
                    tempInt.OnInteraction();
                }
            }
        }
        else
        {
            Reflective[] array = FindObjectsOfType<Reflective>();
            for (int i = 0; i < array.Length; i++)
            {
                array[i].OnEndReflection();
            }

            lineRen.SetPosition(0, shootPoint.position);
            lineRen.SetPosition(1, transform.position + shootPoint.forward * lineLength);
        }

    }


    private void StopLaser()
    {
        lineRen.enabled = false;
    }
}
