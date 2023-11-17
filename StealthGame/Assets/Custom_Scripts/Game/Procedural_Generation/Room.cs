using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Room : MonoBehaviour
{
    public Vector2 size;
    [SerializeField] Color lineColor;
    public ModifiableDoorway[] PossibleDoors;
    public Vector3 bottomLeftPoint, bottomRightPoint, upperLeftPoint, upperRightPoint;
    public Vector3 checkPointLeft, checkPointBottom, checkPointRight, checkPointUp, checkCenter;

    private void Start()
    {
        SetEdgePoints();
    }

    void OnValidate()
    {
        SetEdgePoints();
    }

    void SetEdgePoints()
    {
        bottomLeftPoint = transform.position + new Vector3(-size.x / 2f, 0f, -size.y / 2f);
        bottomRightPoint = transform.position + new Vector3(size.x / 2f, 0f, -size.y / 2f);
        upperLeftPoint = transform.position + new Vector3(-size.x / 2f, 0f, size.y / 2f);
        upperRightPoint = transform.position + new Vector3(size.x / 2f, 0f, size.y / 2f);
        SetCheckPoints();
    }

    void SetCheckPoints()
    {
        checkPointLeft = (bottomLeftPoint + upperLeftPoint) / 2f;
        checkPointRight = (bottomRightPoint + upperRightPoint) / 2f;
        checkPointBottom = (bottomLeftPoint + bottomRightPoint) / 2f;
        checkPointUp = (upperLeftPoint + upperRightPoint) / 2f;
        checkCenter = (upperLeftPoint + bottomRightPoint) / 2f;
    }

    public bool CheckIfRoomOverlapsRoom(Room otherRoom)
    {
        Rect roomA = new Rect(this.transform.position, this.size);
        Rect roomB = new Rect(otherRoom.transform.position, otherRoom.size);

        if (roomA.Overlaps(roomB))
        {
            Debug.Log("Rooms overlap: Room " + this.name + " and Room " + otherRoom.name);
            return true;
        }
        return false;
    }

    public RoomOutgoingDirection GetDoorPosition(ModifiableDoorway checkedDoor)
    {
        if(checkedDoor.transform.position.x == transform.position.x - size.x / 2f)
        {
            return RoomOutgoingDirection.Left;
        }
        else if(checkedDoor.transform.position.x == transform.position.x + size.x / 2f)
        {
            return RoomOutgoingDirection.Right;
        }
        else if(checkedDoor.transform.position.z == transform.position.z - size.y / 2f)
        {
            return RoomOutgoingDirection.Down;
        }
        else if(checkedDoor.transform.position.z == transform.position.z + size.y / 2f)
        {
            return RoomOutgoingDirection.Up;
        }
        return RoomOutgoingDirection.Void;
    }

    public ModifiableDoorway GetDoorOnThisSide(RoomOutgoingDirection checkedSide)
    {
        foreach (ModifiableDoorway door in PossibleDoors)
        {
            switch (checkedSide)
            {
                case RoomOutgoingDirection.Down:
                    if(door.transform.position.z == transform.position.z - size.y / 2f)
                    {
                        return door;
                    }
                    break;
                case RoomOutgoingDirection.Left:
                    if(door.transform.position.x == transform.position.x - size.x / 2f)
                    {
                        return door;
                    }
                    break;
                case RoomOutgoingDirection.Right:
                    if(door.transform.position.x == transform.position.x + size.x / 2f)
                    {
                        return door;
                    }
                    break;
                case RoomOutgoingDirection.Up:
                    if(door.transform.position.z == transform.position.x + size.y / 2f)
                    {
                        return door;
                    }
                    break;
            }
        }
        return null;
    }

    void OnDrawGizmos()
    {
        Debug.DrawLine(bottomLeftPoint, bottomRightPoint, lineColor);
        Debug.DrawLine(bottomLeftPoint, upperLeftPoint, lineColor);
        Debug.DrawLine(bottomRightPoint, upperRightPoint, lineColor);
        Debug.DrawLine(upperLeftPoint, upperRightPoint, lineColor);
        for (int i = 0; i < PossibleDoors.Length; i++)
        {
            Gizmos.DrawWireCube(PossibleDoors[i].transform.position, Vector3.one);
        }
    }
}
