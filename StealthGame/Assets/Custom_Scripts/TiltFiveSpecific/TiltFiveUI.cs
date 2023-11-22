using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TiltFiveUI : MonoBehaviour
{
    public static TiltFiveUI Instance;
    [SerializeField] UIElementConnector curElement, previousElement;
    bool selectionPossible = true;
    public float swapTime = 0.25f;
    public float ConfirmationDelayTime = 1f;
    bool confirmAvailable = true;

    private void Start()
    {
        if(Instance == null)
            Instance = this;
    }

    private void Awake()
    {
        selectionPossible = true;
        confirmAvailable = true;
    }

    public void SelectNewElementAsCurrent(UIElementConnector newElem)
    {
        previousElement = curElement;
        curElement = newElem;
        StartCoroutine(ConfirmationDelay());
    }

    public void SelectToSide(UISelectionDirection newDir)
    {
        UIElementConnector newCurElement = curElement.GetSelectableFromDirection(newDir, curElement);
        UIElementConnector newPreviousElement = curElement;
        ClickOrSwap();
        if (newCurElement != null && newCurElement != curElement)
        {
            curElement = newCurElement;
            previousElement = newPreviousElement;
        }
        HighlightCurButton(curElement.GetSelectable());
    }

    bool Confirm()
    {
        if(TiltFiveInputs.Instance.one || Input.GetKey(KeyCode.Return))
        {
            if (confirmAvailable)
            {
                confirmAvailable = false;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            confirmAvailable = true;
            return false;
        }
    }

    private void Update()
    {
        if(selectionPossible)
        {
            if (Confirm())
            {
                ClickOrSwap();
                CallMethodOf(curElement.GetSelectable());
            }

            if (TiltFiveInputs.Instance.stickY < -0.25f || Input.GetKey(KeyCode.DownArrow))//Down
            {
                SelectToSide(UISelectionDirection.down);
            }
            else if (TiltFiveInputs.Instance.stickY > 0.25f || Input.GetKey(KeyCode.UpArrow))//Up
            {
                SelectToSide(UISelectionDirection.up);
            }
            else if (TiltFiveInputs.Instance.stickX > 0.25f || Input.GetKey(KeyCode.RightArrow))//Right
            {
                SelectToSide(UISelectionDirection.right);
            }
            else if (TiltFiveInputs.Instance.stickX < -0.25f || Input.GetKey(KeyCode.LeftArrow))//Left
            {
                SelectToSide(UISelectionDirection.left);
            }
        }
    }

    void ClickOrSwap()
    {
        StartCoroutine(AvailabilityDelayCo());
    }

    IEnumerator ConfirmationDelay()
    {
        selectionPossible = false;
        yield return new WaitForSeconds(ConfirmationDelayTime);
        selectionPossible = true;
    }

    IEnumerator AvailabilityDelayCo()
    {
        selectionPossible = false;
        yield return new WaitForSeconds(swapTime);
        selectionPossible = true;
    }

    public void HighlightCurButton(Selectable newSel)
    {
        newSel.Select();
    }

    public void CallMethodOf(Selectable newSel) 
    {
        if (newSel is Button)
        {
            newSel.GetComponent<Button>().onClick.Invoke();
        }
        else if (newSel is Toggle)
        {
            newSel.GetComponent<Toggle>().isOn = !newSel.GetComponent<Toggle>().isOn;
        }
    }
}
