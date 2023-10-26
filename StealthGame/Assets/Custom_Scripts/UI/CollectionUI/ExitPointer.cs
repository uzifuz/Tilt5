using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitPointer : MonoBehaviour
{
    private GameObject mainCamera;
    private GameObject worldTargetPosition;

    void Start()
    {
        //gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        worldTargetPosition = GameObject.Find("WinCondition");
        mainCamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCamera != null)
        {
            Vector3 screenPosition = mainCamera.GetComponent<Camera>().WorldToScreenPoint(worldTargetPosition.transform.position);
            Vector3 directionToTarget = screenPosition - transform.position;
            float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
        else
        {
            Debug.LogError("Camera not found");
        }
    }

    public void UpdateRotation()
    {
        gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
    }
}
