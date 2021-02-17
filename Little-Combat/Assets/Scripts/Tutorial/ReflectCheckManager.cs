using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectCheckManager : MonoBehaviour
{
    public TutorialReflect[] reflects;

    int i = 0;

    bool hasTriggered;

    private void Update()
    {
        if (!hasTriggered) 
        {
            foreach (TutorialReflect reflect in reflects)
            {
                if (reflect.isReflectedOn)
                {
                    i++;
                }
            }

            if (i == reflects.Length)
            {
                Debug.Log("Portal... Open...");

                hasTriggered = true;
            }

            i = 0;
        }
    }
}
