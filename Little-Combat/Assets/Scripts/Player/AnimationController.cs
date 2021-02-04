using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        //player moet naar goeie kant kijken
        transform.TransformDirection(FindObjectOfType<PlayerMovement>().gameObject.transform.forward);

        //idle
        if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            //run
            if (Input.GetButton("Running"))
            {
                anim.SetInteger("PlayerState", 2);
            }
            //walk
            else
            {
                anim.SetInteger("PlayerState", 1);
            }
        }
        else
        {
            anim.SetInteger("PlayerState", 0);
        }
        if (!FindObjectOfType<PlayerMovement>().controller.isGrounded)
        {
            anim.SetBool("Jump", true);
        }
    }
    private void LateUpdate()
    {
        if (FindObjectOfType<PlayerMovement>().controller.isGrounded)
        {
            anim.SetBool("Jump", false);
        }
    }
}
