using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    private bool dissolve;
    public Material mat;
    public float speed = 1;
    private float amount;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
        amount = 1;
        mat.SetFloat("_CutoffHeight", amount);
    }

    private void Update()
    {
        if (dissolve)
        {
            if (amount > -1)
            {
                amount -= speed * Time.deltaTime;
                mat.SetFloat("_CutoffHeight", amount);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    public void ActivateDissolve()
    {
        dissolve = true;
    }
}
