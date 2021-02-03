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
        if (Input.GetAxisRaw("Vertical") == 0)
        {
            anim.SetInteger("PlayerState", 0);
        }
        else
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
        if (!FindObjectOfType<PlayerMovement>().controller.isGrounded)
        {
            anim.SetBool("Jump", true);
        }
        //sideways left = 3 right = 4
        if (Input.GetAxisRaw("Horizontal") == 0)
        {

        }
        else
        {
            //right
            if (Input.GetAxisRaw("Horizontal") == -1)
            {
                anim.SetInteger("PlayerState", 4);
            }
            //left
            if (Input.GetAxisRaw("Horizontal") == 1)
            {
                anim.SetInteger("PlayerState", 3);

            }
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
