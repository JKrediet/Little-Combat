using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PressurePlate : MonoBehaviour
{
    public Transform useFunctionFromThis;

    protected GameObject tempSpawned;

    protected float cooldown = 1, nextSpawn;

    public MeshRenderer render;

    // Start is called before the first frame update
    private void Start()
    {
        OnStart();
    }

    public virtual void OnStart()
    {
        render = GetComponent<MeshRenderer>();

        render.material.SetColor("_EmissiveColor", Color.white * 1000);
    }

    protected virtual void OnCollisionEnter(Collision col)
    {
        OnTrigger(col);
    }

    public virtual void OnTrigger(Collision col)
    {
        if (col.gameObject != tempSpawned)
        {
            render.material.SetColor("_EmissiveColor", Color.green * 1000);
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
                        }else if (useFunctionFromThis.GetComponent<PlayableDirector>())
                        {
                            useFunctionFromThis.GetComponent<PlayableDirector>().Play();
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
