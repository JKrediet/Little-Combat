using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public Transform spawnedObject;

    private Transform lastObject;

    public void SpawnObjectIn()
    {
        if(lastObject)
        {
            Destroy(lastObject.gameObject);
        }
        lastObject = Instantiate(spawnedObject, transform.position, transform.rotation);

    }
}
