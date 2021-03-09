using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildStage : MonoBehaviour
{
    //Tutorial_puzzle1
    public GameObject stone1, stone1Pos, stone2, stone2Pos;

    //Tutorial_boss1
    public GameObject boss1;

    //tutorial
    public void Tutorial_puzzle1()
    {
        stone1.transform.position = stone1Pos.transform.position;
        //moet maybe invoke bij
        stone2.transform.position = stone2Pos.transform.position;
    }
    public void Tutorial_boss1()
    {
        //boss meot dood zijn at arrival
    }


    //neptune
    public void Neptune_boss2()
    {
        //boss 2 moet dood zijn
    }
}
