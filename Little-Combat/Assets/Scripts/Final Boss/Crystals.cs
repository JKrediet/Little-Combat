using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystals : MonoBehaviour
{
    public int crystalID;
    public float health;

    private FinalBoss bossie;

    private void Start()
    {
        bossie = FindObjectOfType<FinalBoss>();
        bossie.crystals[crystalID] = crystalID;
    }
    public void TakeDamage()
    {
        health--;
        if(health == 0)
        {
            bossie.crystals[crystalID] = 5;
            if (bossie.CheckCrystals(0, 4) == true)
            {
                bossie.ToNextStage();
            }
            GetComponent<MeshRenderer>().enabled = false;
        }
    }
    public void TurnMeshBackOn()
    {
        GetComponent<MeshRenderer>().enabled = true;
    }
}
