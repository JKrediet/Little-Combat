using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public Transform useFunctionFromThis;

    private float cooldown = 3, nextSpawn;

    private void OnCollisionEnter(Collision collision)
    {
        if (Time.time > nextSpawn)
        {
            if (useFunctionFromThis.GetComponent<SpawnObject>())
            {
                nextSpawn = cooldown + Time.time;
                useFunctionFromThis.GetComponent<SpawnObject>().SpawnObjectIn();
            }
        }
    }
}
