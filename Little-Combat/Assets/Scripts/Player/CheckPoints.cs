using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    public int checkPointsNumber;
    public Transform spawnPoint;

    private void OnTriggerEnter(Collider player)
    {
        if (player.tag == "Player")
        {
            PlayerPrefs.SetInt("LastCheckPoint", checkPointsNumber);
        }
    }
}
