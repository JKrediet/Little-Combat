using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public Transform useFunctionFromThis;

    private GameObject test;

    protected float cooldown = 3, nextSpawn;

    protected MeshRenderer render;

    // Start is called before the first frame update
    void Start()
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
        if (col.gameObject != test)
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
                            test = useFunctionFromThis.GetComponent<SpawnObject>().SpawnObjectIn();
                        }
                    }
                }
            }
        }
    }

    protected virtual void OnCollisionExit(Collision collision)
    {
        render.material.SetColor("_BaseColor", Color.red);
    }
}
