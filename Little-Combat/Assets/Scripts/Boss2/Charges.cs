using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charges : MonoBehaviour
{
    public GameObject flamtrower, otherCharge, lightning1, lightning2;
    public bool charged;

    public void TurnOn()
    {
        charged = true;
        flamtrower.GetComponent<FlameThrower>().isTurnedOn = charged;

        if (otherCharge.GetComponent<Charges>().charged)
        {
            TurnBothOn();
        }
    }
    public void TurnOff()
    {
        charged = false;
        otherCharge.GetComponent<Charges>().charged = false;
        flamtrower.GetComponent<FlameThrower>().isTurnedOn = charged;
        otherCharge.GetComponent<Charges>().flamtrower.GetComponent<FlameThrower>().isTurnedOn = charged;
    }
    public void TurnBothOn()
    {
        flamtrower.GetComponent<FlameThrower>().BothOn();
        otherCharge.GetComponent<Charges>().flamtrower.GetComponent<FlameThrower>().BothOn();
        Invoke("TurnOff", 1);
    }
    private void Update()
    {
        lightning1.SetActive(charged);
        lightning2.SetActive(charged);
    }
}
