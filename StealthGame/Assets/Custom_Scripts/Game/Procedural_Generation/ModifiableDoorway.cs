using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifiableDoorway : MonoBehaviour
{
    [SerializeField] GameObject[] doorway, solidWall, window;
    public ModifiableDoorway ConnectedDoor;
    public enum DoorOutDirection { right = 1, forward = 2, left = -1, back = -2 }
    public DoorOutDirection Direction = DoorOutDirection.left;

    public void AssignDoorDirection()
    {
        if(-transform.right == Vector3.forward)
        {
            Direction = DoorOutDirection.forward;
        }
        else if(-transform.right == Vector3.back)
        {
            Direction = DoorOutDirection.back;
        }
        else if(-transform.right == Vector3.right)
        {
            Direction = DoorOutDirection.right;
        }
        else if(-transform.right == Vector3.left)
        {
            Direction = DoorOutDirection.left;
        }
    }

    public void SetDoor(bool isThereADoor)
    {
        switch(isThereADoor)
        {
            case true:
                for (int i = 0; i < doorway.Length; i++)
                {
                    doorway[i].SetActive(true);
                }
                for (int i = 0; i < solidWall.Length; i++)
                {
                    solidWall[i].SetActive(false);
                }
                for (int i = 0; i < window.Length; i++)
                {
                    window[i].SetActive(false);
                }
                break;
            case false:
                for (int i = 0; i < doorway.Length; i++)
                {
                    doorway[i].SetActive(false);
                }
                if(Random.Range(0f, 1f) < 0.25f && window.Length > 0)
                {
                    for (int i = 0; i < solidWall.Length; i++)
                    {
                        solidWall[i].SetActive(false);
                    }
                }
                else
                {
                    for (int i = 0; i < window.Length; i++)
                    {
                        window[i].SetActive(false);
                    }
                }
                break;
        }
    }
}
