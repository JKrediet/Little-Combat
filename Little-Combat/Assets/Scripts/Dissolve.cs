using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    public Shader dissolve;
    public Renderer rend;
    private float timeToDissolve;
    private bool dissolveTrue;

    private void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        rend.material.shader = dissolve;
    }
    public void DissolveObject()
    {
        dissolveTrue = true;
    }
    private void Update()
    {
        if (dissolveTrue)
        {
            timeToDissolve -= 0.1f * Time.deltaTime;
            rend.material.SetFloat("_CutoffHeight", timeToDissolve); // defaut 1, 1 = full, -1 = empty
        }

        if(Input.GetKeyDown(KeyCode.J))
        {
            dissolveTrue = true;
        }
    }
}
