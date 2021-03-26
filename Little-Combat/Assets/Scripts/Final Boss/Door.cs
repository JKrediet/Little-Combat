using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{

    public DoorHori door1;
    public DoorHori door2;
    public bool ninjaDead, bossDead;

    void OnTriggerEnter()
    {
        if (ninjaDead && bossDead)
        {
            if (door1 != null)
            {
                door1.OpenDoor();
            }

            if (door2 != null)
            {
                door2.OpenDoor();
            }
        }
    }
}
