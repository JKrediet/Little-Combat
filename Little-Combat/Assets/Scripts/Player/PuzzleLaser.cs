﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLaser : MonoBehaviour
{
    public float laserRange;
    public LineRenderer line;
    public LayerMask mask;
    public bool isHitByLaser, mayNotChange, dontDoLaser;
    public Transform laserPos;
    private PuzzleLaser laser;

    void Update()
    {
        //base
        if(mayNotChange)
        {
            isHitByLaser = true;
        }
        if(isHitByLaser)
        {
            //for last in line
            if (!dontDoLaser)
            {
                RaycastHit _hit;
                if (Physics.Raycast(laserPos.position, laserPos.forward, out _hit, laserRange, mask))
                {
                    line.SetPosition(0, laserPos.position);
                    line.SetPosition(1, _hit.point);
                    //turn on hit laser
                    if (_hit.transform.GetComponent<PuzzleLaser>())
                    {
                        if (_hit.transform.GetComponent<PuzzleLaser>().isHitByLaser == false)
                        {
                            laser = _hit.transform.GetComponent<PuzzleLaser>();
                            laser.isHitByLaser = true;
                        }
                    }
                }
                else
                {
                    //turn off when nothing is hitting
                    if (laser != null)
                    {
                        laser.isHitByLaser = false;
                        laser = null;
                    }
                    line.SetPosition(0, laserPos.position);
                    line.SetPosition(1, laserPos.position + laserPos.forward * laserRange);
                }
            }
        }
        else
        {
            //turn off when nothing is hitting
            if (laser != null)
            {
                laser.isHitByLaser = false;
                laser = null;
            }
            line.SetPosition(0, laserPos.position);
            line.SetPosition(1, laserPos.position);
        }
    }
}
