using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateHold : PressurePlate
{
    public Transform moveObject, goHere, originHere;
    private bool giveSignal;
    private GameObject thisOne;

    void Start()
    {
        render = GetComponent<MeshRenderer>();
        render.material.SetColor("_BaseColor", Color.red);

        moveObject.position = originHere.position;
    }

    public override void OnTrigger(Collision col)
    {
        base.OnTrigger(col);

        if (col.gameObject.tag != "Player")
        {
            render.material.SetColor("_BaseColor", Color.green);
            nextSpawn = cooldown + Time.time;
            thisOne = col.gameObject;
            giveSignal = true;
        }
    }


    protected override void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag != "Player")
        {
            giveSignal = false;
            render.material.SetColor("_BaseColor", Color.red);
        }
    }

    private void Update()
    {
        if (giveSignal)
        {
            moveObject.position = new Vector3(originHere.position.x, Mathf.Lerp(moveObject.position.y, goHere.position.y, 0.1f), originHere.position.z);
        }
        if (!giveSignal)
        {
            moveObject.position = new Vector3(originHere.position.x, Mathf.Lerp(moveObject.position.y, originHere.position.y, 0.1f), originHere.position.z);
        }
    }
}
