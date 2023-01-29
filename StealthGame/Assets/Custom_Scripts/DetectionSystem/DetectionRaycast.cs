using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionRaycast : MonoBehaviour
{
    public float viewDistance = 2f;
    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, viewDistance, layerMask))
        {
            var thief = hit.collider.gameObject.GetComponent<Thief>();
            if (thief != null && !thief.IsHidden)
            {
                DetectionHandler.Instance.ThiefDetected = true;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        }
    }
}
