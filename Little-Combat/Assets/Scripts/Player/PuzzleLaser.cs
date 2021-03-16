using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLaser : MonoBehaviour
{
    public float laserRange;
    public LineRenderer line;
    public LayerMask mask;
    public bool isHitByLaser, mayNotChange, dontDoLaser;
    private PuzzleLaser laser;

    void Update()
    {
        if(mayNotChange)
        {
            isHitByLaser = true;
        }
        if(isHitByLaser)
        {
            if (!dontDoLaser)
            {
                RaycastHit _hit;
                if (Physics.Raycast(transform.position, transform.forward, out _hit, laserRange, mask))
                {
                    line.SetPosition(0, transform.position);
                    line.SetPosition(1, _hit.point);
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
                    if (laser != null)
                    {
                        laser.isHitByLaser = false;
                        laser = null;
                    }
                    line.SetPosition(0, transform.position);
                    line.SetPosition(1, transform.position + transform.forward * laserRange);
                }
            }
        }
        else
        {
            if (laser != null)
            {
                laser.isHitByLaser = false;
                laser = null;
            }
            line.SetPosition(0, transform.position);
            line.SetPosition(1, transform.position);
        }
    }
}
