using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    public List<int> crystals;

    private void Awake()
    {
        for(int i = 0; i < 4; i++)
        {
            crystals.Add(0);
        }
    }

    public void Attack()
    {

    }
    public void StopAttack()
    {

    }
}
