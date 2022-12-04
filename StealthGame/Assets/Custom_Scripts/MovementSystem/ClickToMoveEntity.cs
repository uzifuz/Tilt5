using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClickToMoveEntity : MonoBehaviour
{
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
                    Debug.Log($"Hit {hit.collider.name}");
                    if(runTimer > 0)
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
                    //Debug.Log($"Nothing hit");
                }
                currentPlayer.SetAgentDestination(targetLocation, runProxy);
            }
        }
    }
}
