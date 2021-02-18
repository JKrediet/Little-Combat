using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialReflect : Reflective
{
    public bool isReflectedOn;
    public bool hasBeenTriggered;

    protected override void OnStart()
    {
        base.OnStart();

        GetComponent<Renderer>().material.SetColor("_BaseColor", Color.red);
    }

    protected override void OnReflect()
    {
        base.OnReflect();

        GetComponent<Renderer>().material.SetColor("_BaseColor", Color.green);

        isReflectedOn = true;
    }

    protected override void OnEndReflect()
    {
        base.OnEndReflect();

        GetComponent<Renderer>().material.SetColor("_BaseColor", Color.red);

        isReflectedOn = false;
    }
}
