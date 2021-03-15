using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraaiTotem : MonoBehaviour
{
    public float totemNumber;
    public Transform rotateTo;
    private Transform minusOne, plusOne;
    public bool needToRotate;

    private void Start()
    {
        int zelda = (int)totemNumber - 1;
        int sjaakTrekHaar = (int)totemNumber + 1;
        if (zelda < 0)
        {
            zelda = 7;
        }
        if (sjaakTrekHaar > 7)
        {
            sjaakTrekHaar = 0;
        }


        minusOne = FindObjectOfType<Puzzle>().draaiDingetjes[zelda];
        plusOne = FindObjectOfType<Puzzle>().draaiDingetjes[sjaakTrekHaar];

        //debug shit!
        transform.rotation = rotateTo.rotation;
    }
    public void Rotate()
    {
        if (!needToRotate)
        {
            rotateTo.Rotate(new Vector3(0, 90, 0));
            minusOne.GetComponent<DraaiTotem>().rotateTo.Rotate(new Vector3(0, 90, 0));
            plusOne.GetComponent<DraaiTotem>().rotateTo.Rotate(new Vector3(0, 90, 0));

            FindObjectOfType<Puzzle>().MayNotRotate();
            Invoke("StopRotating", 1.1f);
        }
    }
    private void Update()
    {
        if (needToRotate)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateTo.rotation, 90 * Time.deltaTime);
        }
    }
    public void StopRotating()
    {
        FindObjectOfType<Puzzle>().MayRotate();
    }
}
