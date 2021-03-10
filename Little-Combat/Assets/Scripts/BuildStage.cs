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
