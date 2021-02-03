using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    void Update()
    {
        if(Input.GetAxisRaw("Vertical") == 0)
        {
            GetComponent<Animator>().SetInteger("PlayerState", 0);
        }
        else
        {
            if (Input.GetButton("Running"))
            {
                GetComponent<Animator>().SetInteger("PlayerState", 2);
            }
            else
            {
                GetComponent<Animator>().SetInteger("PlayerState", 1);
            }
        }
    }
}
