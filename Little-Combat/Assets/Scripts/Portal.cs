using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Portal : MonoBehaviour
{
    public GameObject loadingScreen;

    public Slider loadingBar;
    public TMP_Text progressText;

    private void OnTriggerEnter(Collider other)
    {
        loadingScreen.SetActive(true);
        PlayerPrefs.SetInt("tutorial_level", 1);
        PlayerPrefs.SetInt("CurrentSavedLevel", 2);

        StartCoroutine(LoadAsychronounsly());
        Debug.Log("Teleport");
    }

    IEnumerator LoadAsychronounsly()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(2);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            loadingBar.value = progress;

            progressText.text = progress * 100f + "%";

            yield return null;
        }
    }

}
