using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialReflect : Reflective
{
    public bool isReflectedOn;

    protected override void OnReflect()
    {
        base.OnReflect();

        isReflectedOn = true;
    }

    protected override void OnEndReflect()
    {
        base.OnEndReflect();

        isReflectedOn = false;
    }
}
