using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteraction : Interaction // This is an interaction. It can be triggerd by the laser the player uses
{
    private MeshRenderer render;

    public GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<MeshRenderer>();

        render.material.SetColor("_BaseColor", Color.red);
    }

    public override void OnInteraction()
    {

        // Call OnInteraction default functionality
        base.OnInteraction();

        // Turn the color of the cube green
        render.material.SetColor("_BaseColor", Color.green);

        door.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
