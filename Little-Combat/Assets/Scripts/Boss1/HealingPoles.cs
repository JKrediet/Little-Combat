using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPoles : MonoBehaviour
{
    public LineRenderer line;
    public GameObject particle;

    public Transform origin;
    public Vector3 offset;

    public Transform boss;
    public Vector3 bossOffset;

    private bool checkForBoss = true;

    private void Update()
    {
        if(checkForBoss)
        {
            line.SetPosition(0, origin.position - offset);
            line.SetPosition(1, boss.position - bossOffset);
        }
    }

    public void DisableLine()
    {
        checkForBoss = false;
        line.enabled = false;

        FindObjectOfType<Boss1>().canBeDamaged = true;

        particle.SetActive(false);
    }
}
