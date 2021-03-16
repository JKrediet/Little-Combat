using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public List<Transform> draaiDingetjes;
    public bool somethingIsRotating, done;

    public void Awake()
    {
        //give all in list own number
        for (int i = 0; i < draaiDingetjes.Count; i++)
        {
            draaiDingetjes[i].GetComponent<DraaiTotem>().totemNumber = i;
        }
    }
    public void MayNotRotate()
    {
        //make sure nothing can rotate if one is
        somethingIsRotating = true;
        for (int i = 0; i < draaiDingetjes.Count; i++)
        {
            draaiDingetjes[i].GetComponent<DraaiTotem>().needToRotate = somethingIsRotating;
        }
    }
    public void MayRotate()
    {
        //done rotating
        somethingIsRotating = false;
        for (int i = 0; i < draaiDingetjes.Count; i++)
        {
            draaiDingetjes[i].GetComponent<DraaiTotem>().needToRotate = somethingIsRotating;
        }
        done = CheckIfAllAreTrue();
    }
    public bool CheckIfAllAreTrue()
    {
        //check if all are on
        for (int i = 0; i < draaiDingetjes.Count; i++)
        {
            if (draaiDingetjes[i].GetComponent<PuzzleLaser>().isHitByLaser == false)
            {
                return false;
            }
        }
        return true;
    }
}
