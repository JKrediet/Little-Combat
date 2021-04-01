using UnityEngine;
using System.Collections;

public class HoriDoorManager : MonoBehaviour {

	public DoorHori door1;
	public DoorHori door2;
    public bool anime;

	void OnTriggerEnter(){
        if (!anime)
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
