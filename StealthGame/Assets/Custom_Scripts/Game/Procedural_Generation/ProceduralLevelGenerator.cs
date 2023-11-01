using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class ProceduralLevelGenerator : MonoBehaviour
{
    public float MaxNumberOfRoomsCreated;
    int curNumberOfCreatedRooms = 0;
    public GameObject[] possibleRooms;
    public string RoomProgress;
    [SerializeField] Room startingRoom;
    [SerializeField] List<Room> allGeneratedRooms = new List<Room>();
    List<ModifiableDoorway> allAvailableDoors = new List<ModifiableDoorway>();
    NavMeshSurface curSurface;

    private void Start()
    {
        curSurface = FindObjectOfType<NavMeshSurface>();
        StartCoroutine("RoomGenCo");
    }

    IEnumerator RoomGenCo()
    {
        if(CreateRoom())
        {
            Debug.Log("Room has been created");
            RoomProgress = $"Progress: {((curNumberOfCreatedRooms / MaxNumberOfRoomsCreated) * 100f).ToString("00.0")} %";
        }
        else
        {
            Debug.Log("Room has NOT been created");
        }
        if(curNumberOfCreatedRooms < MaxNumberOfRoomsCreated)
        {
            yield return new WaitForSeconds(0.05f);
            StartCoroutine("RoomGenCo");
        }
        else
        {
            SwapDoorStates();
            curSurface.BuildNavMesh();
            CollectibleMaster.Instance.SetupCollectionSystem();
            allGeneratedRooms.Clear();
        }
    }

    bool CreateRoom()
    {
        Debug.Log("CreateRoom");
        //Choose Room to connect new Room
        Room possibleExistingRoom = ChooseExistingRoomForConnection();
        //--> Failure: No Room to connect is available. Return false
        //Choose new Room to be created, and instantiate it
        Room newlyCreatedRoom = Instantiate(SelectRoomToBeInstantiated().gameObject, transform.position, transform.rotation).GetComponent<Room>();
        //--> Failure: How would this even fail?!?!??!?
        //Align new Room for Door to Door connection
        if(AlignNewRoomToDoor(possibleExistingRoom?.GetRandomUnconnectedDoor(), newlyCreatedRoom))
        {
            curNumberOfCreatedRooms++;
            allGeneratedRooms.Add(newlyCreatedRoom);
            return true;
        }
        else
        {
            //Debug.Log("OverlapDetected");
            //Not correctly assigned or overlapping
            Destroy(newlyCreatedRoom.gameObject);
        }
        
        return false;
    }

    Room ChooseExistingRoomForConnection()
    {
        Debug.Log("ChooseExistingRoomForConnection");
        if (curNumberOfCreatedRooms == 0)
            return null;
        //If not the start
        Room returnRoom = null;
        int safetyCounter = 0;
        do
        {
            int ranNr = Random.Range(0, allGeneratedRooms.Count);
            if (allGeneratedRooms[ranNr].HasUnconnectedDoors())
            {
                returnRoom = allGeneratedRooms[ranNr];
            }
            safetyCounter++;
        }
        while (returnRoom == null && safetyCounter < 100);
        return returnRoom;
    }

    Room SelectRoomToBeInstantiated()
    {
        Debug.Log("SelectRoomToBeInstantiated");
        if(curNumberOfCreatedRooms > 0)
        {
            return possibleRooms[Random.Range(0, possibleRooms.Length)].GetComponent<Room>();
        }
        else
        {
            return startingRoom;
        }
    }

    Room RotateRoom(Room rotateThisRoom)
    {
        Debug.Log("RotateRoom");
        float randomRotation = Mathf.Round(Random.Range(0, 4)) * 90f;

        if (randomRotation != 0f && randomRotation != 180f)
            rotateThisRoom.size = new Vector2(rotateThisRoom.size.y, rotateThisRoom.size.x);

        rotateThisRoom.transform.eulerAngles = new Vector3(0f, randomRotation, 0f);
        rotateThisRoom.ReassignDoorDirections();
        return rotateThisRoom;
    }

    bool AlignNewRoomToDoor(ModifiableDoorway alignedDoor, Room newRoom)
    {
        Debug.Log("AlignNewRoomToDoor");
        ModifiableDoorway newRoomDoor = newRoom.GetRandomUnconnectedDoor();
        if (newRoomDoor == null)
            return false;
        else
            newRoomDoor.ConnectedDoor = alignedDoor;

        if(alignedDoor != null)
        {
            int whileExitCheck = 0;
            while ((int)newRoomDoor.Direction != -(int)alignedDoor.Direction && whileExitCheck < 8)
            {
                RotateRoom(newRoom);
                whileExitCheck++;
            }

            if (alignedDoor.transform.position != newRoomDoor.transform.position)
            {
                newRoom.transform.position = alignedDoor.transform.position;
                Vector3 doorsOffset = newRoomDoor.transform.position - alignedDoor.transform.position;
                newRoom.transform.position -= doorsOffset;
            }

            if (CheckRoomOverlapping(newRoom))
            {
                //Debug.Log("Alignement failed due to Overlap");
                return false;
            }
            else
            {
                alignedDoor.ConnectedDoor = newRoomDoor;
                return true;
            }
        }
        else
        {
            return true;
        }
        
    }

    /// <summary>
    /// Update the door list to only include available doors
    /// </summary>
    /// <returns>Returns true if there are still available doors left, false if not</returns>
    bool UpdateDoorList()
    {
        /*foreach(ModifiableDoorway door in allAvailableDoors)
        {
            if(door.ConnectedDoor != null)
            {
                allAvailableDoors.Remove(door);
                Debug.Log($"Removed {door.name}");
            }
        }*/
        if(allAvailableDoors.Count > 0)
        {
            return true;
        }
        return false;
    }


    void SwapDoorStates()
    {
        ModifiableDoorway[] allDoors = FindObjectsOfType<ModifiableDoorway>();
        //Debug.Log("Func => SwapDoorStates with " + allDoors.Length + " doors");
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
                        allDoors[i].ConnectedDoor = allDoors[j];
                        allDoors[j].ConnectedDoor = allDoors[i];
                    }
                    else
                    {
                        float distance = Vector3.Distance(allDoors[j].transform.position, allDoors[i].transform.position);
                        //Debug.Log("Door Distance too great at " + distance);
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
                //They overlap
                return true;
            }
        }
        //They do not overlap
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
