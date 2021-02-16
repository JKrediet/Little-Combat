using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPoles : MonoBehaviour
{
    public LineRenderer line;
    public Vector3 offset;
    public Transform boss;
    public Vector3 bossOffset;

    private void Update()
    {
        line.SetPosition(0, transform.position - offset);
        line.SetPosition(1, boss.position - bossOffset);
    }

    private void DisableLine()
    {
        line.enabled = false;

        FindObjectOfType<Boss1>().canBeDamaged = true;
    }
}
