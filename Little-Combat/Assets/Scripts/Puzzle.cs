using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public List<Transform> draaiDingetjes;
    public bool somethingIsRotating, done;
    public GameObject finalBoss;
    public Transform bossPosRot, door;

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
        if(done == true)
        {
            PlayerPrefs.SetInt("neptune_laserpuzzle", 1);
            door.GetComponent<HoriDoorManager>().anime = true;
            SpawnBoss();
        }
    }
    public void SpawnBoss()
    {
        Instantiate(finalBoss, bossPosRot.position, bossPosRot.rotation);
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
    private void Update()
    {
        if (done == true)
        {
            transform.Translate(-transform.up * Time.deltaTime);
            if (transform.position.y <= -10)
            {
                Destroy(gameObject);
            }
        }
    }
}
