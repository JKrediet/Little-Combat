using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    bool isPaused;

    public GameObject pauseScreen;
    public GameObject settingsScreen;

    public Behaviour[] disableOnPause;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsScreen.activeSelf == true)
            {
                pauseScreen.SetActive(true);
                settingsScreen.SetActive(false);
            }
            else
            {
                isPaused = !isPaused;
                pauseScreen.SetActive(true);
            }
        }

        if (isPaused)
        {
            //pauseScreen.SetActive(true);
            for (int i = 0; i < disableOnPause.Length; i++)
            {
                disableOnPause[i].enabled = false;
            }
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
        else
        {
            pauseScreen.SetActive(false);
            for (int i = 0; i < disableOnPause.Length; i++)
            {
                disableOnPause[i].enabled = true;
            }
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }
    }

    public void Resume()
    {
        isPaused = false;
    }

    public void Settings()
    {
        pauseScreen.SetActive(false);
        settingsScreen.SetActive(true);
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }

    public void SettingsOff()
    {
        pauseScreen.SetActive(true);
        settingsScreen.SetActive(false);
    }

    public void Back()
    {
        settingsScreen.SetActive(false);
        pauseScreen.SetActive(true);
    }
}
