using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DetectionRaycast : MonoBehaviour
{
    public float viewDistance = 2f;
    public LayerMask layerMask;

    public float maxViewAngle = 15f;
    Vector3 currentEulerRotation;
    bool viewingPlayer = false;

    LineRenderer lineRen;

    // Start is called before the first frame update
    void Start()
    {
        lineRen = GetComponentInChildren<LineRenderer>();
        lineRen.transform.SetParent(null);
        lineRen.transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

        currentEulerRotation = new Vector3(Random.Range(-maxViewAngle / 4, maxViewAngle / 4), Random.Range(-maxViewAngle / 2, maxViewAngle / 2), 0);
        transform.localEulerAngles = currentEulerRotation;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, viewDistance, layerMask))
        {
            Debug.DrawLine(transform.position, hit.point, Color.green);
            var thief = hit.collider.gameObject.GetComponent<Thief>();
            if (thief != null && !thief.IsHidden)
            {
                DetectionHandler.Instance.ThiefDetected = true;
            }
            else
            {
                if(thief != null)
                {
                    Debug.LogWarning("Thief is hidden!");
                }
            }
            viewingPlayer = true;
        }
        else
        {
            viewingPlayer = false;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        }
        RenderLine(hit.point);
    }

    void RenderLine(Vector3 destination)
    {
        lineRen.positionCount = 2;
        lineRen.SetPosition(0, transform.position);
        lineRen.SetPosition(1, destination);
    }
}
