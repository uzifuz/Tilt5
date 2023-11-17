using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltFiveBoardMover : MonoBehaviour
{
    public static TiltFiveBoardMover Instance;
    public Transform gameBoard;
    public bool boardLocked = false;
    public float boardMoveSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
            Instance = this;
    }

    public void MoveBoard()
    {
        if (!boardLocked)
        {
            gameBoard.transform.position = CameraMovement.Instance.transform.position;
            gameBoard.transform.rotation = CameraMovement.Instance.transform.rotation;
        }
    }
}
