using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToMoveEntity : MonoBehaviour
{
    [SerializeField]
    ControllableEntity currentPlayer;
    [SerializeField]
    LayerMask clickableObjects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SendPlayerToLocation();
    }

    void SendPlayerToLocation()
    {
        if(currentPlayer != null)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                Vector3 targetLocation = Vector3.up * 100f;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickableObjects))
                {
                    targetLocation = hit.point;
                    Debug.Log($"Hit {hit.collider.name}");
                }
                else
                {
                    Debug.Log($"Nothing hit");
                }
                currentPlayer.SetAgentDestination(targetLocation);
            }
        }
    }
}
