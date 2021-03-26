using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FadeToBlack : MonoBehaviour
{
    public GameObject image;

    public void StartFade()
    {
        GetComponent<Animator>().SetTrigger("New Trigger");
    }

    public void OnFaded()
    {
        SceneManager.LoadScene(3);
    }
}
