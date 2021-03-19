using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystals : MonoBehaviour
{
    public int crystalID;
    public float health;

    private void Start()
    {
        FindObjectOfType<FinalBoss>().crystals[crystalID] = crystalID;
    }
    public void TakeDamage()
    {
        health--;
        if(health == 0)
        {
            FindObjectOfType<FinalBoss>().crystals[crystalID] = crystalID;
        }
    }
}
