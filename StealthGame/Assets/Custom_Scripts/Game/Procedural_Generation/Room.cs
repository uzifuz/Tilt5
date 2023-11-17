using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Room : MonoBehaviour
{
    public bool connectingSegment;
    public Transform[] DoorAlignedPoints;
    public GameObject LevelExitObj;
    public Vector2 size;
    [SerializeField] Color lineColor;
    public ModifiableDoorway[] PossibleDoors;
    public Vector3 bottomLeftPoint, bottomRightPoint, upperLeftPoint, upperRightPoint;
    public Vector3 checkPointLeft, checkPointBottom, checkPointRight, checkPointUp, checkCenter;
    Bounds boundsA, boundsB;


    private void Start()
    {
        SetEdgePoints();
        ReassignDoorDirections();
    }

    void OnValidate()
    {
        SetEdgePoints();
    }

    public void ReassignDoorDirections()
    {
        foreach (ModifiableDoorway door in PossibleDoors)
        {
            door.AssignDoorDirection();
        }
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
        boundsA = new Bounds(this.transform.position, new Vector3(size.x, 2f, size.y));
        boundsB = new Bounds(otherRoom.transform.position, new Vector3(otherRoom.size.x, 2f, otherRoom.size.y));

        bool xOverlap = boundsA.min.x < boundsB.max.x && boundsA.max.x > boundsB.min.x;
        bool zOverlap = boundsA.min.z < boundsB.max.z && boundsA.max.z > boundsB.min.z;

        if (xOverlap && zOverlap)
        {
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

    public ModifiableDoorway GetRandomUnconnectedDoor()
    {
        List<ModifiableDoorway> unconnectedDoors = new List<ModifiableDoorway>();
        for (int i = 0; i < PossibleDoors.Length; i++)
        {
            if (PossibleDoors[i].ConnectedDoor == null)
                unconnectedDoors.Add(PossibleDoors[i]);
        }

        if (unconnectedDoors.Count == 0)
            return null;
        else
            return unconnectedDoors[Random.Range(0, unconnectedDoors.Count)];
    }

    public bool HasUnconnectedDoors()
    {
        for (int i = 0; i < PossibleDoors.Length; i++)
        {
            if (PossibleDoors[i].ConnectedDoor == null)
                return true;
        }
        return false;
    }

    public ModifiableDoorway GetDoorConnectedTo(ModifiableDoorway otherDoor)
    {
        for (int i = 0; i < PossibleDoors.Length; i++)
        {
            if (PossibleDoors[i].ConnectedDoor == otherDoor)
                return PossibleDoors[i];
        }
        return null;
    }

    void OnDrawGizmos()
    {
        Debug.DrawLine(bottomLeftPoint, bottomRightPoint, lineColor);
        Debug.DrawLine(bottomLeftPoint, upperLeftPoint, lineColor);
        Debug.DrawLine(bottomRightPoint, upperRightPoint, lineColor);
        Debug.DrawLine(upperLeftPoint, upperRightPoint, lineColor);

        Gizmos.DrawWireCube(boundsA.center, boundsA.size);
        Gizmos.DrawWireCube(boundsB.center, boundsB.size);

        for (int i = 0; i < PossibleDoors.Length; i++)
        {
            Gizmos.DrawWireCube(PossibleDoors[i].transform.position, Vector3.one);
        }
    }
}
