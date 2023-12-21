using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTrigger : MonoBehaviour
{
    InteractableObject currentObject;
    public KeyCode interactionKey = KeyCode.F;

    private void Update()
    {
        bool tiltFiveInteractPressed = false;
        if(TiltFive.Wand.TryGetWandDevice(TiltFive.PlayerIndex.One, TiltFive.ControllerIndex.Right, out TiltFive.WandDevice wandDevice))
        {
            if(wandDevice.One.isPressed)
            {
                tiltFiveInteractPressed = true;
            }
        }
        if(currentObject != null)
        {
            if (Input.GetKeyDown(interactionKey) || tiltFiveInteractPressed)
            {
                currentObject.Interact();
            }
            if(Input.GetKey(interactionKey) || tiltFiveInteractPressed)
            {
                //Debug.Log("Key is held");
                currentObject.ButtonHeldDown();
            }
            else if(Input.GetKeyUp(interactionKey) || !tiltFiveInteractPressed)
            {
                //Debug.Log("Key is put up");
                currentObject.ButtonUp();
            }
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
