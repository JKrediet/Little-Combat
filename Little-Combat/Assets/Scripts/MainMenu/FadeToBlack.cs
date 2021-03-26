using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FadeToBlack : MonoBehaviour
{
    public GameObject image;


    public void StartFade()
    {
        image.SetActive(true);
    }

    public void OnFaded()
    {
        SceneManager.LoadScene(3);
    }
}
