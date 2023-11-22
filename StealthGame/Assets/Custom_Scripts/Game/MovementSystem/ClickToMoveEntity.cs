using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickToMoveEntity : MonoBehaviour
{
    public Transform camMaster, wandTipTransform, wandGripTransform;
    bool wandButtonPressed = false;
    [SerializeField]
    ControllableEntity currentPlayer;
    [SerializeField]
    LayerMask clickableObjects;
    [SerializeField]
    float runTime = 0.5f;
    float runTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SendPlayerToLocation();
        if(runTimer >= 0)
        {
            runTimer -= Time.deltaTime;
        }
    }

    public Vector3 WandDirection()
    {
        Vector3 wandTipNoY = new Vector3(wandTipTransform.position.x, 0, wandTipTransform.position.z);
        Vector3 wandGripNoY = new Vector3(wandGripTransform.position.x, 0, wandGripTransform.position.z);
        Vector3 direction = (wandTipNoY - wandGripNoY).normalized;
        return direction;
    }

    void SendPlayerToLocation()
    {
        if(currentPlayer != null && currentPlayer.CanMove)
        {
            if(PlayerPrefs.GetInt("Tilt5Mode") == 1)
            {
                //Debug.Log("Rotating Tilt5 Active");
                camMaster.rotation = Quaternion.LookRotation(WandDirection(), Vector3.up);
            }
            else
            {
                //Debug.Log($"Rotating in Standalone mode");
                camMaster.eulerAngles += new Vector3(0f, (Input.GetKey(KeyCode.Q) ? -1f : 0f) + (Input.GetKey(KeyCode.E) ? 1f : 0f) ,0f);
            }
            Vector3 posMod = camMaster.transform.forward * (Input.GetAxis("Vertical") + TiltFiveInputs.Instance.stickY)  + camMaster.transform.right * (Input.GetAxis("Horizontal") + TiltFiveInputs.Instance.stickX) + Vector3.up * -1f;
            Vector3 newPos = currentPlayer.transform.position + posMod;
            //Debug.DrawLine(currentPlayer.transform.position, newPos);
            currentPlayer.SetAgentDestination(newPos, Input.GetKey(KeyCode.LeftShift) || TiltFiveInputs.Instance.trigger);
            
            TiltFiveBoardMover.Instance.MoveBoard();
                
            
        }
    }
}
