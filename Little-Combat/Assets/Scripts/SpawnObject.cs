using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public Transform spawnedObject;

    public void SpawnObjectIn()
    {
        Instantiate(spawnedObject, transform.position, transform.rotation);
    }
}
