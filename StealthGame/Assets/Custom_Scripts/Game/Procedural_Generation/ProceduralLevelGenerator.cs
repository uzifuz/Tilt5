using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ProceduralLevelGenerator : MonoBehaviour
{
    public int MaxNumberOfRoomsCreated;
    public Vector2 MaxAreaSize;
    public float MinUnitSize = 5f;
    int curNumberOfCreatedRooms = 0;
    public GameObject[] possibleRooms;
    [SerializeField] Room startingRoom;
    [SerializeField] List<Room> allGeneratedRooms = new List<Room>();

    private void Update()
    {
        int checkCounter = 0;
        if (checkCounter < MaxNumberOfRoomsCreated * 10 && curNumberOfCreatedRooms < MaxNumberOfRoomsCreated)
        {
            checkCounter++;
            if (CreateRoom())
            {
                //
            }
            else
            {
                checkCounter++;
            }
        }
        if(curNumberOfCreatedRooms == MaxNumberOfRoomsCreated)
        {
            SwapDoorStates();
            curNumberOfCreatedRooms++;
        }
    }

    Room SelectRoomToBeInstantiated(Room refRoom)
    {
        Room newRoom;
        Debug.Log("Func => SelectRoomToBeInstantiated at " + transform.position);
        if(curNumberOfCreatedRooms > 0)
        {
            newRoom = Instantiate(possibleRooms[Random.Range(0, possibleRooms.Length)], transform.position, transform.rotation).GetComponent<Room>();
            newRoom.name = "Room_Clone_" + allGeneratedRooms.Count.ToString();
            float randomRotation = Mathf.Round(Random.Range(1, 4)) * 90f;
            if (randomRotation != 0f && randomRotation != 180f)
                newRoom.size = new Vector2(newRoom.size.y, newRoom.size.x);

            newRoom.transform.eulerAngles = new Vector3(0f, randomRotation, 0f);
            int randomCreationPoint = Random.Range(0, refRoom.DoorAlignedPoints.Length);
            Vector3 creationPointToCenter = refRoom.DoorAlignedPoints[0].position - refRoom.DoorAlignedPoints[randomCreationPoint].position;
            Vector3 positionOffset = GetCreationPoint((refRoom.size.x + newRoom.size.x) / 2f - creationPointToCenter.x, (refRoom.size.y + newRoom.size.y) / 2f - creationPointToCenter.z);

            newRoom.transform.position = refRoom.DoorAlignedPoints[randomCreationPoint].position + positionOffset;
        }
        else
        {
            newRoom = Instantiate(refRoom, Vector3.zero, transform.rotation).GetComponent<Room>(); ;
            newRoom.name = "Starting_Room_Clone_";
        }
        return newRoom;
    }

    bool CreateRoom()
    {
        if(curNumberOfCreatedRooms < MaxNumberOfRoomsCreated)//
        {
            if(allGeneratedRooms.Count == 0)//First Room to be created
            {
                allGeneratedRooms.Add(SelectRoomToBeInstantiated(startingRoom));
                curNumberOfCreatedRooms++;
            }
            else//Any other room
            {
                Room possibleRoom = SelectRoomToBeInstantiated(allGeneratedRooms[Random.Range(0, curNumberOfCreatedRooms)]);
                if(!CheckRoomOverlapping(possibleRoom))
                {
                    allGeneratedRooms.Add(possibleRoom);
                    curNumberOfCreatedRooms++;
                }
                else
                {
                    Destroy(possibleRoom.gameObject);
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
        Debug.Log("Func => SwapDoorStates with " + allDoors.Length + " doors");
        foreach(ModifiableDoorway door in allDoors)
        {
            door.SetDoor(false);
        }
        for (int i = 0; i < allDoors.Length; i++)
        {
            for (int j = 0; j < allDoors.Length; j++)
            {
                if (allDoors[i] != allDoors[j])
                {
                    if (Vector3.Distance(allDoors[j].transform.position, allDoors[i].transform.position) <= 0.15f)
                    {
                        allDoors[i].SetDoor(true);
                        allDoors[j].SetDoor(true);
                        allDoors[i].ConnectsToThisRoom = GetRoomFromDoor(allDoors[j]);
                        allDoors[j].ConnectsToThisRoom = GetRoomFromDoor(allDoors[i]);
                    }
                    else
                    {
                        float distance = Vector3.Distance(allDoors[j].transform.position, allDoors[i].transform.position);
                        Debug.Log("Door Distance too great at " + distance);
                    }
                }
            }
        }
    }

    bool CheckRoomOverlapping(Room thisRoom)
    {
        foreach(Room room in allGeneratedRooms)
        {
            if(room.CheckIfRoomOverlapsRoom(thisRoom))
            {
                Debug.LogWarning(thisRoom.name + " conflicting with " + room.name + " at "  + thisRoom.transform.position + "/" + room.transform.position + ", about to be deleted");
                return true;
            }
        }
        return false;
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

    Vector3 GetCreationPoint(float sizeOffsetX, float sizeOffsetZ)
    {
        bool generateAtXAxis = true;
        bool generateAtNegativeAxis = false;
        Vector3 returnOffset = Vector3.zero;
        if(Random.Range(0f, 1f) < 0.5f)
            generateAtXAxis = false;
        if(Random.Range(0f, 1f) < 0.5f)
            generateAtNegativeAxis = false;

        //
        if (generateAtXAxis)
        {
            returnOffset = Vector3.right * sizeOffsetX;
        }
        else
        {
            returnOffset = Vector3.forward * sizeOffsetZ;
        }

        return generateAtNegativeAxis ? returnOffset : returnOffset * -1f;
    }
}
