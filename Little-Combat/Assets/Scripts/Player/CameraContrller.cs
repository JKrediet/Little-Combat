﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContrller : MonoBehaviour
{
    public Transform cameraLookat, cameraLaserPosition;
    public float lookSpeed, maxLookAngle, collisionOffset = 0.2f;
    public bool isHoldingLaser;

    //privates
    private Vector2 rotation;
    private Vector3 defaultPos, directionNormalized;
    private float defaultDistance;

    private void Start()
    {
        defaultPos = transform.localPosition;
        directionNormalized = defaultPos.normalized;
        defaultDistance = Vector3.Distance(defaultPos, Vector3.zero);
    }
    private void Update()
    {
        CameraMovement();
        CameraPosCheck();
    }
    private void CameraMovement()
    {
        rotation.y += Input.GetAxis("Mouse Y") * lookSpeed;
        rotation.x += Input.GetAxis("Mouse X") * lookSpeed;
        if(isHoldingLaser)
        {
            rotation.y = Mathf.Clamp(rotation.y, -maxLookAngle, -maxLookAngle);
        }
        else
        {
            rotation.y = Mathf.Clamp(rotation.y, -maxLookAngle, maxLookAngle);
        }
        cameraLookat.localRotation = Quaternion.Euler(-rotation.y, rotation.x, 0);
    }
    private void CameraPosCheck()
    {
        Vector3 currentPos = defaultPos;
        RaycastHit hit;
        Vector3 dirTmp = cameraLookat.TransformPoint(defaultPos) - cameraLookat.position;
        if (Physics.SphereCast(cameraLookat.position, collisionOffset, dirTmp, out hit, defaultDistance))
        {
            currentPos = (directionNormalized * (hit.distance - collisionOffset));
        }
        if (isHoldingLaser)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, cameraLaserPosition.localPosition, Time.deltaTime * 15f);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, currentPos, Time.deltaTime * 15f);
        }
    }
}
