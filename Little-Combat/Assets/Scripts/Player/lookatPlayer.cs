using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookatPlayer : MonoBehaviour
{
    public Transform player;
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rot = player.position - transform.position;
        rot.y = 0;
        transform.rotation = Quaternion.LookRotation(rot);
    }
}
