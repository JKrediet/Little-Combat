using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHealth : MonoBehaviour
{
    public void DoDamage()
    {
        Destroy(gameObject);
    }
}
