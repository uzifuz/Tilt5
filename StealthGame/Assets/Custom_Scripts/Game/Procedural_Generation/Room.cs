using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Room : MonoBehaviour
{
    public float xSize = 1, zSize = 1;
    [SerializeField] Color lineColor;
    public ModifiableDoorway[] PossibleDoors;
    public Vector3 bottomLeftPoint, bottomRightPoint, upperLeftPoint, upperRightPoint;

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
        bottomLeftPoint = transform.position + new Vector3(-xSize / 2f, 0f, -zSize / 2f);
        bottomRightPoint = transform.position + new Vector3(xSize / 2f, 0f, -zSize / 2f);
        upperLeftPoint = transform.position + new Vector3(-xSize / 2f, 0f, zSize / 2f);
        upperRightPoint = transform.position + new Vector3(xSize / 2f, 0f, zSize / 2f);
    }

    public bool CheckIfPointIsInsideRectangle(Vector3 checkedPoint)
    {
        if(checkedPoint.x < bottomRightPoint.x && checkedPoint.x > bottomLeftPoint.x && //Horizontally Inside
            checkedPoint.z < upperLeftPoint.z && checkedPoint.z > bottomLeftPoint.z)//Vertically inside
        {
            Debug.LogError("POINT INSIDE OTHER GEOMETRY; DELETE THIS!!!");
            return true;
        }
        return false;
    }

    public RoomOutgoingDirection GetDoorPosition(ModifiableDoorway checkedDoor)
    {
        if(checkedDoor.transform.position.x == transform.position.x - xSize / 2f)
        {
            return RoomOutgoingDirection.Left;
        }
        else if(checkedDoor.transform.position.x == transform.position.x + xSize / 2f)
        {
            return RoomOutgoingDirection.Right;
        }
        else if(checkedDoor.transform.position.z == transform.position.z - zSize / 2f)
        {
            return RoomOutgoingDirection.Down;
        }
        else if(checkedDoor.transform.position.z == transform.position.z + zSize / 2f)
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
                    if(door.transform.position.z == transform.position.z - zSize / 2f)
                    {
                        return door;
                    }
                    break;
                case RoomOutgoingDirection.Left:
                    if(door.transform.position.x == transform.position.x - xSize / 2f)
                    {
                        return door;
                    }
                    break;
                case RoomOutgoingDirection.Right:
                    if(door.transform.position.x == transform.position.x + xSize / 2f)
                    {
                        return door;
                    }
                    break;
                case RoomOutgoingDirection.Up:
                    if(door.transform.position.z == transform.position.x + zSize / 2f)
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
