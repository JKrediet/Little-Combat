using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentCheckPoint;
    public GameObject playerPrefab;

    private Vector3 spawnPosition;
    private List<GameObject> checkPoints;

    //areas cleared
    public bool tutorial_puzzle1, tutorial_boss1, neptune_boss2;
    //levels cleared
    public bool tutorial_level, neptune_level;

    private void Awake()
    {
        ClearedStages();
        BuildStages();
        SpawnPlayerAtCheckpoint();
    }
    private void SpawnPlayerAtCheckpoint()
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

    private void ClearedStages()
    {
        //tutorial
        PlayerPrefs.GetInt("tutorial_puzzle1", 0);
        PlayerPrefs.GetInt("tutorial_boss1", 0);
        PlayerPrefs.GetInt("tutorial_level", 0);

        //neptune
        PlayerPrefs.GetInt("neptune_boss2", 0);
        PlayerPrefs.GetInt("neptune_level", 0);
    }

    private void BuildStages()
    {

    }
}
