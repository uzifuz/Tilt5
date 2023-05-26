using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionRaycast : MonoBehaviour
{
    public float viewDistance = 2f;
    public LayerMask layerMask;

    public float maxViewAngle = 15f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.localEulerAngles = new Vector3(Random.Range(-maxViewAngle / 2, maxViewAngle / 2), Random.Range(-maxViewAngle / 2, maxViewAngle / 2), 0);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, viewDistance, layerMask))
        {
            Debug.DrawLine(transform.position, hit.point, Color.green);
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
