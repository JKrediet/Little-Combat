using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflective : MonoBehaviour
{
    public LineRenderer lineRen;

    public Transform shootPoint;

    public LayerMask laserMask;

    public float lineLength;

    private Reflective tempRef = null;


    private void Update()
    {
        
    }

    public void OnReflection()
    {
        lineRen.enabled = true;

        RaycastHit _hit;

        if(Physics.Raycast(shootPoint.position, shootPoint.forward, out _hit, lineLength, laserMask))
        {
            lineRen.SetPosition(0, shootPoint.position);
            lineRen.SetPosition(1, _hit.point);

            if (_hit.transform.GetComponent<Reflective>())
            {
                _hit.transform.GetComponent<Reflective>().OnReflection();
                tempRef = _hit.transform.GetComponent<Reflective>();
            }
            else
            {
                Interaction tempInt = _hit.transform.GetComponent<Interaction>();
                if(tempInt != null)
                {
                    tempInt.OnInteraction();
                }
            }
        }
        else
        {
            lineRen.SetPosition(0, shootPoint.transform.position);
            lineRen.SetPosition(1, shootPoint.transform.position + shootPoint.transform.forward * lineLength);

            if (tempRef != null)
            {
                tempRef.StopLaser();
            }
        }
    }

    public void StopLaser()
    {
        lineRen.enabled = false;
    }
}
