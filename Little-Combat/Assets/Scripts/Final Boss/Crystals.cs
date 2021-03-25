using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystals : MonoBehaviour
{
    public int crystalID;
    public float health;

    private FinalBoss bossie;

    //dissolve
    private bool dissolveOn, dissolveOff;
    private Material mat;
    public float speed = 1;
    private float amount;

    private void Start()
    {
        bossie = FindObjectOfType<FinalBoss>();
        bossie.crystals[crystalID] = crystalID;

        mat = GetComponent<Renderer>().material;
        amount = 1;
        mat.SetFloat("_CutoffHeight", amount);
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
            //turn crystal mesh off
            DissolveOff();
        }
    }
    public void RestoreCrystal()
    {
        DissolveOn();
        bossie.crystals[crystalID] = crystalID;
        health = 3;
    }

    //dissolve 
    private void Update()
    {
        if (dissolveOn)
        {
            if (amount < 1)
            {
                amount += speed * Time.deltaTime;
                mat.SetFloat("_CutoffHeight", amount);
            }
            else
            {
                amount = 1;
                dissolveOn = false;
            }
        }
        if (dissolveOff)
        {
            if (amount > -1)
            {
                amount -= speed * Time.deltaTime;
                mat.SetFloat("_CutoffHeight", amount);
            }
            else
            {
                amount = -1;
                dissolveOff = false;
            }
        }
    }
    public void DissolveOn()
    {
        dissolveOn = true;
    }
    public void DissolveOff()
    {
        dissolveOff = true;
    }
}
