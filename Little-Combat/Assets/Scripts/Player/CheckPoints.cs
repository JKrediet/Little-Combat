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
    public AudioSource activationSound;


    private bool canOpen;

    private bool timeDown;

    private void OnTriggerEnter(Collider player)
    {
        if (player.tag == "Player")
        {
            activationSound.Play();

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
            if (Input.GetKeyDown(KeyCode.F) && !timeDown)
            {
                timeDown = true;

                GetComponent<GuidingStatue>().ResetInfo();
                panel.SetActive(!panel.activeSelf);

                StartCoroutine(TypeSentence(GetComponent<GuidingStatue>().GetInfo()));
            }
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        text.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            text.text += letter;
            yield return null;
        }

        timeDown = false;
    }
}
