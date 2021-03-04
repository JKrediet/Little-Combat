using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject loadingScreen;
    public Slider loadingBar;

    public void OnLoadLevel()
    {
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        StartCoroutine(LoadAsychronounsly());
    }

    IEnumerator LoadAsychronounsly()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(0);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            loadingBar.value = progress;
            // Do progress text

            yield return null;
        }
    }
}
