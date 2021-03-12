using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject loadingScreen;
    public GameObject settingsScreen;

    public Button continueButton;
    public Button quitButton;

    public GameObject quitMessage;

    public Slider loadingBar;
    public TMP_Text progressText;

    private void Start()
    {
        if (PlayerPrefs.GetInt("NewGameStarted") == 1)
        {
            continueButton.interactable = true;
        }

        mainMenu.SetActive(true);
        settingsScreen.SetActive(false);
        loadingScreen.SetActive(false);
        quitMessage.SetActive(false);
    }

    public void OpenSettings()
    {
        mainMenu.SetActive(false);
        loadingScreen.SetActive(false);
        settingsScreen.SetActive(true);
    }

    public void OnContinue()
    {
        StartCoroutine(LoadAsychronounsly(PlayerPrefs.GetInt("CurrentSavedLevel")));
    }

    public void OpenMainMenu()
    {
        mainMenu.SetActive(true);
        loadingScreen.SetActive(false);
        settingsScreen.SetActive(false);
    }

    public void OnNewGame()
    {

        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("NewGameStarted", 1);
        PlayerPrefs.SetInt("CurrentSavedLevel", 1);

        StartCoroutine(LoadAsychronounsly(PlayerPrefs.GetInt("CurrentSavedLevel")));
    }

    public void ToggleQuit()
    {
        quitMessage.SetActive(!quitMessage.activeSelf);
        quitButton.interactable = !quitButton.interactable;
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    IEnumerator LoadAsychronounsly(int level)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(level);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            loadingBar.value = progress;

            progressText.text = progress * 100f + "%";

            yield return null;
        }
    }
}
