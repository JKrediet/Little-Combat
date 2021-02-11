using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateHold : PressurePlate
{
    public Transform moveObject, goHere, originHere;
    private bool giveSignal;

    public override void OnStart()
    {
        base.OnStart();

        moveObject.position = originHere.position;
    }

    public override void OnTrigger(Collision col)
    {
        base.OnTrigger(col);

        if (col.gameObject.tag != "Player")
        {
            nextSpawn = cooldown + Time.time;
            giveSignal = true;
        }
    }

    public override void OnTriggerEnd(Collision collision)
    {
        base.OnTriggerEnd(collision);

        if (collision.gameObject.tag != "Player")
        {
            giveSignal = false;

            if (tempSpawned != null)
            {
                Destroy(tempSpawned);
            }
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
