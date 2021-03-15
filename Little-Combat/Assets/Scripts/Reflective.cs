using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflective : MonoBehaviour
{
    public LineRenderer lineRen;

    public Transform shootPoint;

    public LayerMask laserMask;

    public float lineLength;


    private void Update()
    {
        //lineRen.enabled = false;
    }

    public void OnReflection()
    {
        RaycastHit _hit;

        if(Physics.Raycast(shootPoint.position, shootPoint.forward, out _hit, lineLength, laserMask))
        {
            lineRen.SetPosition(0, shootPoint.position);
            lineRen.SetPosition(1, _hit.point);

            if (_hit.transform.GetComponent<Reflective>())
            {
                _hit.transform.GetComponent<Reflective>().OnReflection();
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
            lineRen.SetPosition(0, transform.position);
            lineRen.SetPosition(1, transform.position + transform.forward * lineLength);
        }
    }
}
