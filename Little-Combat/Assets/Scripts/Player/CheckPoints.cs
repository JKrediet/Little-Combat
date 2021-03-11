using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    public int checkPointsNumber;
    public Transform spawnPoint;

    public GameObject canvas;

    private bool canOpen;

    private void OnTriggerEnter(Collider player)
    {
        if (player.tag == "Player")
        {
            canOpen = true;

            canvas.SetActive(true);

            PlayerPrefs.SetInt("LastCheckPoint", checkPointsNumber);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canOpen = false;

            canvas.SetActive(false);
        }
    }
}
