using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GuidingStatue : MonoBehaviour
{
    private string text;

    private int totem;

    string[] guiding;
    string sentence = "";

    private void Start()
    {
        totem = GetComponent<CheckPoints>().checkPointsNumber;

        StreamReader reader = new StreamReader("Assets/Files/GuidingTotem.txt");

        guiding = reader.ReadToEnd().Split('\n');

        for (int i = 0; i < guiding.Length; i++)
        {
            if(guiding[i].StartsWith("#" + totem))
            {
                string[] line = guiding[i].Split(' ');

                for (int a = 1; a < line.Length; a++)
                {
                    if (sentence != "")
                    {
                        sentence = sentence + " " + line[a];
                    }
                    else
                    {
                        sentence = sentence + line[a];
                    }

                }
            }
        }
    }
    
    public void ReadTotem()
    {
        print(sentence);
    }
}
