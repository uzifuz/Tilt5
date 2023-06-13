using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickToMoveEntity : MonoBehaviour
{
    public GameObject wandTip;
    public Transform gameBoard, wandTipTransform, wandGripTransform;
    public Light torchLight;
    public float boardMoveSpeed = 1f;
    bool wandButtonPressed = false;
    [SerializeField]
    ControllableEntity currentPlayer;
    [SerializeField]
    LayerMask clickableObjects;
    [SerializeField]
    float runTime = 0.5f;
    float runTimer = 0f;
    public bool boardLocked = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SendPlayerToLocation();
        CheckIfClickable();
        if(runTimer >= 0)
        {
            runTimer -= Time.deltaTime;
        }
    }

    void CheckIfClickable()
    {
        Ray ray = new Ray(wandTip.transform.position, wandTip.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickableObjects))
        {
            torchLight.color = Color.green;
        }
        else
        {
            torchLight.color = Color.red;
        }
    }

    void SendPlayerToLocation()
    {
        bool runProxy = false;
        if(currentPlayer != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && currentPlayer.CanMove)
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
                    Debug.Log($"RunProxy{runProxy}");
                    currentPlayer.SetAgentDestination(hit.point, runProxy);
                }
                if(!wandDevice.Trigger.IsPressed(0.5f) && wandButtonPressed)
                {
                    wandButtonPressed = false;
                }

                Vector3 wandTipNoY = new Vector3(wandTipTransform.position.x, 0, wandTipTransform.position.z);
                Vector3 wandGripNoY = new Vector3(wandGripTransform.position.x, 0, wandGripTransform.position.z);
                Vector3 direction = (wandTipNoY - wandGripNoY).normalized;
                if(!boardLocked)
                    gameBoard.Translate(Vector3.Cross(direction, gameBoard.up) * wandDevice.Stick.ReadValue().x * Time.deltaTime * boardMoveSpeed + direction * wandDevice.Stick.ReadValue().y * Time.deltaTime * -boardMoveSpeed);
                
            }
        }
    }
}
