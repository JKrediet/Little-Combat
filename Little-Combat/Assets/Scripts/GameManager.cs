using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int currentCheckPoint;
    public GameObject playerPrefabTutorial, playerPrefabNeptune;

    private Vector3 spawnPosition;
    private List<GameObject> checkPoints;

    //areas cleared
    public int tutorial_puzzle1, tutorial_boss1, neptune_boss2;
    public int tutorial_puzzle1_1;

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
            checkPoints[check.GetComponent<CheckPoints>().number] = check;
        }
        spawnPosition = checkPoints[currentCheckPoint].GetComponent<CheckPoints>().spawnPoint.position;
        if(currentCheckPoint >= checkPoints.Count)
        {
            currentCheckPoint = 0;
        }
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            Instantiate(playerPrefabTutorial, spawnPosition, checkPoints[currentCheckPoint].GetComponent<CheckPoints>().spawnPoint.rotation);
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            Instantiate(playerPrefabNeptune, spawnPosition, checkPoints[currentCheckPoint].GetComponent<CheckPoints>().spawnPoint.rotation);
        }
    }

    private void ClearedStages()
    {
        //tutorial
        tutorial_puzzle1 = PlayerPrefs.GetInt("tutorial_puzzle1", 0);
        tutorial_boss1 = PlayerPrefs.GetInt("tutorial_boss1", 0);

        //neptune
        neptune_boss2 = PlayerPrefs.GetInt("neptune_boss2", 0);
    }

    private void BuildStages()
    {
        if(tutorial_puzzle1 > 0)
        {
            GetComponent<BuildStage>().Tutorial_puzzle1();
        }
        if (tutorial_boss1 > 0)
        {
            GetComponent<BuildStage>().Tutorial_boss1();
        }
    }
    private void Update()
    {
        if(tutorial_puzzle1_1 > 1)
        {
            PlayerPrefs.SetInt("tutorial_puzzle1", 1);
        }
    }
}
