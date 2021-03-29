using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform shootPoint;
    public LineRenderer lineRen;

    public Transform laserEffect;

    public LayerMask laserMask;

    public float lineLength;

    private Transform cam;
    private Reflective tempRef = null;

    private bool reflected;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
    }

    // Update is called every frame after update
    void LateUpdate()
    {
        ShootLaser();
    }

    private void ShootLaser()
    {
        RaycastHit _hit;

        if(Physics.Raycast(shootPoint.position, shootPoint.forward, out _hit, lineLength, laserMask))
        {
            lineRen.SetPosition(0, shootPoint.transform.position);
            lineRen.SetPosition(1, _hit.point);

            if (_hit.transform.GetComponent<Reflective>())
            {
                _hit.transform.GetComponent<Reflective>().OnReflection();
                tempRef = _hit.transform.GetComponent<Reflective>();
            }
            else
            {
                Interaction tempInt = _hit.transform.GetComponent<Interaction>();
                if (tempInt != null)
                {
                    tempInt.OnInteraction();
                }
            }
        }
        else
        {
            lineRen.SetPosition(0, shootPoint.transform.position);
            lineRen.SetPosition(1, shootPoint.transform.position + shootPoint.transform.forward * lineLength);

            if(tempRef != null)
            {
                tempRef.StopLaser();
            }
        }
    }


    private void StopLaser()
    {
        lineRen.enabled = false;
    }
}
