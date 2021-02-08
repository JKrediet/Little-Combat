using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflective : MonoBehaviour
{
    public LineRenderer lineRen;

    public Transform laserEffect;

    private void Update()
    {
        lineRen.enabled = false;
    }

    public void OnReflection(Vector3 hitPoint, Vector3 direction, Vector3 normal)
    {
        lineRen.enabled = true;

        Vector3 reflect = Vector3.Reflect(direction, normal);

        Ray ray = new Ray(hitPoint, reflect);

        RaycastHit _hit;

        if(Physics.Raycast(ray, out _hit))
        {
            lineRen.SetPosition(0, hitPoint);
            lineRen.SetPosition(1, _hit.point);

            laserEffect.gameObject.SetActive(true);
            laserEffect.position = _hit.point;

            if (_hit.transform.GetComponent<Reflective>())
            {
                _hit.transform.GetComponent<Reflective>().OnReflection(_hit.point, reflect, _hit.normal);
            }
            else
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
            laserEffect.gameObject.SetActive(false);

            lineRen.SetPosition(0, hitPoint);
            lineRen.SetPosition(1, transform.position + reflect * 1000f);
        }
    }

    public void OnEndReflection()
    {
        lineRen.enabled = false;
    }
}
