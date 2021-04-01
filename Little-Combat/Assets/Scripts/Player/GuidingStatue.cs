using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GuidingStatue : MonoBehaviour
{
    //TextAreaAttribute(int minLines, int maxLines);
    [TextArea(15,20)]
    public string textInfo;

    public string GetInfo()
    {
        return textInfo;
    }
}
