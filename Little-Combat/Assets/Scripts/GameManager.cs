using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentCheckPoint;
    public GameObject playerPrefab;

    private Vector3 spawnPosition;
    private List<GameObject> checkPoints;

    private void Awake()
    {
        currentCheckPoint = PlayerPrefs.GetInt("LastCheckPoint", 0);
        GameObject[] tempList = GameObject.FindGameObjectsWithTag("CheckPoint");
        checkPoints = new List<GameObject>(tempList);
        foreach (GameObject check in tempList)
        {
            checkPoints[check.GetComponent<CheckPoints>().checkPointsNumber] = check;
        }
        spawnPosition = checkPoints[currentCheckPoint].GetComponent<CheckPoints>().spawnPoint.position;
        Instantiate(playerPrefab, spawnPosition, checkPoints[currentCheckPoint].GetComponent<CheckPoints>().spawnPoint.rotation);
    }
}
