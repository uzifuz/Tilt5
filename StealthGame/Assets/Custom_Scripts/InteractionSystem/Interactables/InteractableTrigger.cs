using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTrigger : MonoBehaviour
{
    InteractableObject currentObject;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && currentObject != null)
        {
            currentObject.Interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Interactable")
        {
            currentObject = other.GetComponent<InteractableObject>();
            currentObject.ToggleObjectHighlight(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Interactable")
        {
            other.GetComponent<InteractableObject>().ToggleObjectHighlight(false);
            currentObject = null;
        }
    }
}
