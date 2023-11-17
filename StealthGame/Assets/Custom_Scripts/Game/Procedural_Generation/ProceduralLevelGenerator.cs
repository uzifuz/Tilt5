using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ProceduralLevelGenerator : MonoBehaviour
{
    public int MaxNumberOfRoomsCreated;
    int curNumberOfCreatedRooms = 0;
    public GameObject[] possibleRooms;
    [SerializeField] List<Room> allGeneratedRooms = new List<Room>();

    /*private void Start()
    {
        int checkCounter = 0;
        while(checkCounter < MaxNumberOfRoomsCreated * 10 && curNumberOfCreatedRooms < MaxNumberOfRoomsCreated)
        {
            checkCounter++;
            if(CreateRoom())
            {
                curNumberOfCreatedRooms++;
            }
            else
            {
                checkCounter++;
            }
        }
        SwapDoorStates();
    }*/

    private void Update()
    {
        int checkCounter = 0;
        if (checkCounter < MaxNumberOfRoomsCreated * 10 && curNumberOfCreatedRooms < MaxNumberOfRoomsCreated)
        {
            checkCounter++;
            if (CreateRoom())
            {
                curNumberOfCreatedRooms = allGeneratedRooms.Count;
            }
            else
            {
                checkCounter++;
            }
        }
        if(curNumberOfCreatedRooms == MaxNumberOfRoomsCreated)
        {
            SwapDoorStates();
        }
    }

    Room SelectRoomForInstantiated()
    {
        Room newRoom = Instantiate(possibleRooms[Random.Range(0, possibleRooms.Length)], transform.position, transform.rotation).GetComponent<Room>();
        newRoom.name = "Room_Clone_" + allGeneratedRooms.Count.ToString();
        return newRoom;
    }

    ModifiableDoorway CheckForUnsetDoor()
    {
        ModifiableDoorway[] allDoors = FindObjectsOfType<ModifiableDoorway>();
        ModifiableDoorway selectedDoor = allDoors[Random.Range(0, allDoors.Length)];
        int checkCounter = 0;
        while(true)
        {
            if(checkCounter > 100)
            {
                break;
            }
            checkCounter++;
            if(selectedDoor.ConnectsToThisRoom == null)
            {
                break;
            }
            selectedDoor = allDoors[Random.Range(0, allDoors.Length)];
        }
        return selectedDoor;
    }

    ModifiableDoorway CheckForDoorCompatibility(Room checkedRoom, RoomOutgoingDirection possibleConnectionFrom)
    {
        RoomOutgoingDirection requiredDoorSide = RoomOutgoingDirection.Void;
        switch(possibleConnectionFrom)
        {
            case RoomOutgoingDirection.Right:
                requiredDoorSide = RoomOutgoingDirection.Left;
                break;
            case RoomOutgoingDirection.Left:
                requiredDoorSide = RoomOutgoingDirection.Right;
                break;
            case RoomOutgoingDirection.Down:
                requiredDoorSide = RoomOutgoingDirection.Up;
                break;
            case RoomOutgoingDirection.Up:
                requiredDoorSide= RoomOutgoingDirection.Down;
                break;
        }
        return checkedRoom.GetDoorOnThisSide(requiredDoorSide);
    }

    bool CreateRoom()
    {
        if(curNumberOfCreatedRooms < MaxNumberOfRoomsCreated)//
        {
            if(allGeneratedRooms.Count == 0)
            {
                allGeneratedRooms.Add(SelectRoomForInstantiated());
            }
            else
            {
                Room newRoom = SelectRoomForInstantiated();
                ModifiableDoorway connectingDoor = CheckForUnsetDoor();
                Room connectingRoom = GetRoomFromDoor(connectingDoor);
                if(connectingRoom == null)
                {
                    Debug.LogError("CreateRoom ABORTED. Cause: No available connecting Room found");
                    Destroy(newRoom.gameObject);
                    return false;
                }
                RoomOutgoingDirection connectionSide = connectingRoom.GetDoorPosition(connectingDoor);
                ModifiableDoorway newRoomDoor = CheckForDoorCompatibility(newRoom, connectionSide);
                if (newRoomDoor != null)
                {
                    SetRoomOnThisSide(connectingRoom, newRoom, connectionSide);
                    if(CheckIfRoomFits(newRoom))
                    {
                        allGeneratedRooms.Add(newRoom);
                        connectingDoor.ConnectsToThisRoom = newRoom;
                        newRoomDoor.ConnectsToThisRoom = connectingRoom;
                        SwapDoorStates();
                        Debug.Log(GetRoomFromDoor(connectingDoor).name + " connects to " + GetRoomFromDoor(newRoomDoor));
                        return true;
                    }
                    else
                    {
                        Debug.LogError("CreateRoom ABORTED. Cause: Room violated another room's space");
                        Destroy(newRoom.gameObject);
                    }
                }
                else
                {
                    Debug.LogError("CreateRoom ABORTED. Cause: Door incompatible");
                    Destroy(newRoom.gameObject);
                    return false;
                }
            }
        }
        else
        {
            Debug.LogError("CreateRoom ABORTED. Cause: Already enough rooms");
            return false;
        }
        return false;
    }

    void SwapDoorStates()
    {
        ModifiableDoorway[] allDoors = FindObjectsOfType<ModifiableDoorway>();
        foreach(ModifiableDoorway door in allDoors)
        {
            door.SetDoor(false);
        }
        for (int i = 0; i < allDoors.Length; i++)
        {
            for (int j = 0; j < allDoors.Length; j++)
            {
                if (allDoors[j].ConnectsToThisRoom != null && allDoors[i].ConnectsToThisRoom != null)
                {
                    if (Vector3.Distance(allDoors[j].transform.position, allDoors[i].transform.position) <= 0.001f)
                    {
                        allDoors[i].SetDoor(true);
                        allDoors[j].SetDoor(true);
                        allDoors[i].ConnectsToThisRoom = GetRoomFromDoor(allDoors[j]);
                        allDoors[j].ConnectsToThisRoom = GetRoomFromDoor(allDoors[i]);
                    }
                }
                else
                {
                    continue;
                }
            }
        }
    }

    void SetRoomOnThisSide(Room originalRoom, Room newRoom, RoomOutgoingDirection sideOfOriginalRoomDoor)
    {
        Vector3 offset = Vector3.zero;
        switch (sideOfOriginalRoomDoor)
        {
            case RoomOutgoingDirection.Left:
                offset = (originalRoom.size.x / 2f + newRoom.size.x / 2f) * Vector3.left;
                break;
            case RoomOutgoingDirection.Right:
                offset = (originalRoom.size.x / 2f + newRoom.size.x / 2f) * Vector3.right;
                break;
            case RoomOutgoingDirection.Up:
                offset = (originalRoom.size.y / 2f + newRoom.size.y / 2f) * Vector3.forward;
                break;
            case RoomOutgoingDirection.Down:
                offset = (originalRoom.size.y / 2f + newRoom.size.y / 2f) * Vector3.back;
                break;
        }
        newRoom.transform.position = originalRoom.transform.position + offset;
    }

    bool CheckIfRoomFits(Room thisRoom)
    {
        foreach(Room room in allGeneratedRooms)
        {
            if(room.CheckIfRoomOverlapsRoom(thisRoom))
            {
                Debug.LogWarning(thisRoom.name + " conflicting with " + room.name + ", about to be deleted");
                return false;
            }
        }
        return true;
    }

    Room GetRoomFromDoor(ModifiableDoorway door)
    {
        foreach(Room room in allGeneratedRooms)
        {
            if(room.PossibleDoors.Contains(door))
            {
                return room;
            }
        }
        return null;
    }
}
