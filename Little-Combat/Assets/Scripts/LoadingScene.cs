using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public Slider loadingBar;

    public void Start()
    {
        StartCoroutine(LoadAsychronounsly());
    }

    IEnumerator LoadAsychronounsly()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(3);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            loadingBar.value = progress;
            // Do progress text

            yield return null;
        }
    }
}
