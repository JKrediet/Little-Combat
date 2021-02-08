using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject spawnedObject;

    private GameObject lastObject;

    public GameObject SpawnObjectIn()
    {
        if(lastObject)
        {
            Destroy(lastObject.gameObject);
        }
        lastObject = Instantiate(spawnedObject, transform.position, transform.rotation);

        return lastObject;

    }
}
