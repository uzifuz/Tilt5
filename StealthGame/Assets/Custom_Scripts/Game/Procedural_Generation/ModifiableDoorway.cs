using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifiableDoorway : MonoBehaviour
{
    [SerializeField] GameObject doorway, solidWall;
    public Room ConnectsToThisRoom;
    public RoomOutgoingDirection OutGoingDirection;

    public void SetDoor(bool isThereADoor)
    {
        doorway.SetActive(isThereADoor);
        solidWall.SetActive(!isThereADoor);
    }
}
