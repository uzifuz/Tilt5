using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifiableDoorway : MonoBehaviour
{
    [SerializeField] GameObject[] doorway, solidWall, window;
    public Room ConnectsToThisRoom;
    public RoomOutgoingDirection OutGoingDirection;

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
                if(Random.Range(0f, 1f) < 0.25f)
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
