using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickToMoveEntity : MonoBehaviour
{
    public GameObject wandTip;
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

    void SendPlayerToLocation()
    {
        if(currentPlayer != null && currentPlayer.CanMove)
        {
            Vector3 posMod = camMaster.transform.forward * Input.GetAxis("Vertical") + camMaster.transform.right * Input.GetAxis("Horizontal") + Vector3.up * -1f;
            Vector3 newPos = currentPlayer.transform.position + posMod;
            //Debug.DrawLine(currentPlayer.transform.position, newPos);
            currentPlayer.SetAgentDestination(newPos, Input.GetKey(KeyCode.LeftShift));
            //Debug.Log($"New Pos: {posMod}");

            /*if (Input.GetKeyDown(KeyCode.Mouse0) && currentPlayer.CanMove)
            {
                Vector3 targetLocation = Vector3.up * 100f;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickableObjects))
                {
                    targetLocation = hit.point;
                    if (runTimer > 0)
                    {
                        runProxy = true;
                    }
                    else
                    {
                        runTimer = runTime;
                    }
                }
                else
                {
                    Debug.Log($"Nothing hit");
                }
                currentPlayer.SetAgentDestination(targetLocation, runProxy);
            }
            if(TiltFive.Wand.TryGetWandDevice(TiltFive.PlayerIndex.One, TiltFive.ControllerIndex.Right, out TiltFive.WandDevice wandDevice))
            {
                if(wandDevice.Trigger.IsPressed(0.5f) && !wandButtonPressed)
                {
                    wandButtonPressed = true;
                    //Debug.Log("This shit was pressed!!!" + Random.Range(0f, 1f));
                    Ray ray = new Ray(wandTip.transform.position, wandTip.transform.forward);
                    RaycastHit hit;
                    if(Physics.Raycast(ray, out hit, Mathf.Infinity, clickableObjects))
                    {
                        //Debug.Log(hit.collider.gameObject.name);
                        if (runTimer > 0)
                        {
                            runProxy = true;
                        }
                        else
                        {
                            runTimer = runTime;
                        }
                    }
                    else
                    {
                        Debug.Log("Nothing was hit");
                    }
                    currentPlayer.SetAgentDestination(hit.point, runProxy);
                }
                if(!wandDevice.Trigger.IsPressed(0.5f) && wandButtonPressed)
                {
                    wandButtonPressed = false;
                }*/

                Vector3 wandTipNoY = new Vector3(wandTipTransform.position.x, 0, wandTipTransform.position.z);
                Vector3 wandGripNoY = new Vector3(wandGripTransform.position.x, 0, wandGripTransform.position.z);
                Vector3 direction = (wandTipNoY - wandGripNoY).normalized;
                TiltFiveBoardMover.Instance.MoveBoard();
                
            
        }
    }
}
