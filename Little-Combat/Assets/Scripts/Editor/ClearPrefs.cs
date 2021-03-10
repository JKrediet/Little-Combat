using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ClearPrefs : MonoBehaviour
{
    [MenuItem("PlayerPrefs/ClearAllPrefs")]
    static void ClearData()
    {
        PlayerPrefs.DeleteAll();
    }
}
