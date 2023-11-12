using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElementConnector : MonoBehaviour
{
    public bool executeOnSelect = false, revertToPreviousOnExecute = false;
    [SerializeField] Selectable associatedSelectable;
    [SerializeField] UIElementConnector current;
    [SerializeField] UIElementConnector left, right, up, down;

    private void OnEnable()
    {
        associatedSelectable = GetComponent<Selectable>();
    }

    public UIElementConnector GetSelectableFromDirection(UISelectionDirection newDir, UIElementConnector previous)
    {
        switch(newDir)
        {
            case UISelectionDirection.left: return CheckAvailability(left, previous);
            case UISelectionDirection.right: return CheckAvailability(right, previous);
            case UISelectionDirection.up: return CheckAvailability(up, previous);
            case UISelectionDirection.down: return CheckAvailability(down, previous);
            default: return left;
        }
    }

    UIElementConnector CheckAvailability(UIElementConnector newSel, UIElementConnector previous)
    {
        if (newSel == null)
            return previous;
        //
        if(newSel.executeOnSelect)
        {
            if(newSel.associatedSelectable is Button)
            {
                newSel.associatedSelectable.GetComponent<Button>().onClick.Invoke();
            }
            else if(newSel.associatedSelectable is Toggle)
            {
                newSel.associatedSelectable.GetComponent<Toggle>().isOn = !newSel.associatedSelectable.GetComponent<Toggle>().isOn;
            }
        }
        //
        if(newSel.revertToPreviousOnExecute)
        {
            Debug.Log($"Returning Prev {previous.name}");
            return previous;
        }
        else
        {
            Debug.Log($"Returning New {previous.name}");
            return newSel;
        }
    }

    public Selectable GetSelectable()
    {
        return associatedSelectable;
    }
}
