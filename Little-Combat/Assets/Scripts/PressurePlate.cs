using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public Transform useFunctionFromThis;

    protected GameObject tempSpawned;

    protected float cooldown = 3, nextSpawn;

    protected MeshRenderer render;

    // Start is called before the first frame update
    private void Start()
    {
        OnStart();
    }

    public virtual void OnStart()
    {
        render = GetComponent<MeshRenderer>();

        render.material.SetColor("_BaseColor", Color.red);
    }

    protected virtual void OnCollisionEnter(Collision col)
    {
        OnTrigger(col);
    }

    public virtual void OnTrigger(Collision col)
    {
        if (col.gameObject != tempSpawned)
        {
            render.material.SetColor("_BaseColor", Color.green);
            if (Time.time > nextSpawn)
            {
                if (col.gameObject.tag != "Player")
                {
                    if (useFunctionFromThis)
                    {
                        if (useFunctionFromThis.GetComponent<SpawnObject>())
                        {
                            nextSpawn = cooldown + Time.time;
                            tempSpawned = useFunctionFromThis.GetComponent<SpawnObject>().SpawnObjectIn();
                        }
                    }
                }
            }
        }
    }

    protected virtual void OnCollisionExit(Collision collision)
    {
        OnTriggerEnd(collision);
    }

    public virtual void OnTriggerEnd(Collision collision)
    {
        render.material.SetColor("_BaseColor", Color.red);
    }
}
