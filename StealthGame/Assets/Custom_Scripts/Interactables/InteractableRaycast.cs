using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableRaycast : MonoBehaviour
{
    public float rayCastRadius = 5f;
    InteractableObject currentObject;
    [SerializeField]
    LayerMask raycastLayers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, rayCastRadius, Vector3.forward, out hit, Mathf.Infinity, raycastLayers, (int)QueryTriggerInteraction.UseGlobal))
        {
            Debug.Log($"Hit {hit.collider.name} {hit.collider.gameObject.layer}");
            if(currentObject != null && currentObject != hit.collider.gameObject.GetComponent<InteractableObject>())
            {
                currentObject.ToggleObjectHighlight(false);
            }
            Debug.Log($"Found something true {hit.collider.name}");
            currentObject = hit.collider.gameObject.GetComponent<InteractableObject>();
            if(currentObject != null)
            {
                Debug.Log($"Assigned new object {currentObject.name}");
                currentObject.ToggleObjectHighlight(true);
            }
        }
        else
        {
            if(currentObject != null)
            {
                Debug.Log($"Nothing hit {hit.collider.name}");
                currentObject.ToggleObjectHighlight(false);
                currentObject = null;
            }
        }
        if (Input.GetKeyDown(KeyCode.E) && currentObject != null)
        {
            currentObject.Interact();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, rayCastRadius);
    }
}
