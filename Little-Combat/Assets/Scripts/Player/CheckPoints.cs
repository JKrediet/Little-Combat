using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    public int checkPointsNumber;
    public Transform spawnPoint;

    public GameObject canvas;

    public GameObject panel;
    public TMPro.TMP_Text text;

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

    private void Update()
    {
        if (canOpen)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                GetComponent<GuidingStatue>().ResetInfo();
                panel.SetActive(!panel.activeSelf);
                text.text = GetComponent<GuidingStatue>().GetInfo();
            }
        }
    }
}
