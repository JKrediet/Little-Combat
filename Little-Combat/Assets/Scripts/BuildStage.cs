using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildStage : MonoBehaviour
{
    [Header("TUTORIAL")]
    //Tutorial_puzzle1
    public GameObject stone1;
    public GameObject stone1Pos;
    public GameObject stone2;
    public GameObject stone2Pos;
    //Tutorial_boss1
    public GameObject boss1;
    public GameObject deadBoss1;
    public GameObject equipment;
    public GameObject body;
    public GameObject particleDing;
    public GameObject pillar1;
    public GameObject pillar1Pos;
    public GameObject pillar2;
    public GameObject pillar2Pos;
    public GameObject pillar3;
    public GameObject pillar3Pos;

    //tutorial
    public void Tutorial_puzzle1()
    {
        if(stone1 != null)
        {
            stone1.transform.position = stone1Pos.transform.position;
            Invoke("Tutorial_puzzle12", 0.4f);
        }
    }
    private void Tutorial_puzzle12()
    {
        if (stone2 != null)
        {
            stone2.transform.position = stone2Pos.transform.position;
        }
    }
    public void Tutorial_boss1()
    {
        if(boss1 != null)
        {
            Instantiate(deadBoss1, boss1.transform.position, boss1.transform.rotation);
            pillar1.GetComponent<HealingPoles>().DisableLine();
            pillar2.GetComponent<HealingPoles>().DisableLine();
            pillar3.GetComponent<HealingPoles>().DisableLine();
            pillar1.transform.position = pillar1Pos.transform.position;
            pillar2.transform.position = pillar2Pos.transform.position;
            pillar3.transform.position = pillar3Pos.transform.position;
            Destroy(equipment);
            Destroy(body);
            Destroy(particleDing);
            Invoke("Tutorial_boss12", 1);
        }
    }
    public void Tutorial_boss12()
    {
        boss1.GetComponent<Boss1>().KillBoss();
    }


    //neptune
    public void Neptune_boss2()
    {
        //boss 2 moet dood zijn
    }
}
