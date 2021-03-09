using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class Credits : MonoBehaviour
{
    public float scrollSpeed;
    public Transform moveObject;

    [Header("Title")]
    public string title;
    public TMP_Text titleText;

    [Header("Names")]
    [TextArea]
    public string credits;

    public TMP_Text creditText;

    private void Start()
    {
        string path = "Assets/Files/credits.txt";

        StreamReader reader = new StreamReader(path);

        creditText.text = reader.ReadToEnd();
    }

    private void Update()
    {
        moveObject.Translate(0f, scrollSpeed, 0f);
    }
}
