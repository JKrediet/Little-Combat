using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public List<Transform> draaiDingetjes;
    public bool somethingIsRotating;
    
    public void Awake()
    {
        for(int i = 0; i < draaiDingetjes.Count; i++)
        {
            draaiDingetjes[i].GetComponent<DraaiTotem>().totemNumber = i;
        }
    }
    public void MayNotRotate()
    {
        somethingIsRotating = true;
        for (int i = 0; i < draaiDingetjes.Count; i++)
        {
            draaiDingetjes[i].GetComponent<DraaiTotem>().needToRotate = somethingIsRotating;
        }
    }
    public void MayRotate()
    {
        somethingIsRotating = false;
        for (int i = 0; i < draaiDingetjes.Count; i++)
        {
            draaiDingetjes[i].GetComponent<DraaiTotem>().needToRotate = somethingIsRotating;
        }
    }
}
