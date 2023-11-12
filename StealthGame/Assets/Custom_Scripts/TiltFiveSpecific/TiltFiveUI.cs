using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TiltFiveUI : MonoBehaviour
{
    [SerializeField] UIElementConnector curElement, previousElement;
    [SerializeField] Selectable[] allButtons;
    bool selectionPossible = true;
    public int curIndex = 0;
    public float swapTime = 0.33f;
    float swapTimer = 0;

    public void SelectNewElementAsCurrent(UIElementConnector newElem)
    {
        previousElement = curElement;
        curElement = newElem;
    }

    public void SelectToSide(UISelectionDirection newDir)
    {
        Debug.Log($"Selected {newDir.ToString()}");
        UIElementConnector newCurElement = curElement.GetSelectableFromDirection(newDir, curElement);
        Debug.Log($"NewCurElement {newCurElement.name}");
        UIElementConnector newPreviousElement = curElement;
        ClickOrSwap();
        if (newCurElement != null && newCurElement != curElement)
        {
            Debug.Log($"New element set {newCurElement != null} && {newCurElement != curElement}");
            curElement = newCurElement;
            previousElement = newPreviousElement;
        }
        HighlightCurButton(curElement.GetSelectable());
    }

    private void Update()
    {
        if(selectionPossible)
        {
            if (TiltFiveInputs.Instance.one || Input.GetKeyDown(KeyCode.Return))
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
        StartCoroutine("AvailabilityDelayCo");
    }

    IEnumerator AvailabilityDelayCo()
    {
        selectionPossible = false;
        yield return new WaitForSeconds(swapTime);
        selectionPossible = true;
    }

    /*private void Update()
    {
        if(TiltFiveInputs.Instance.one)
        {
            CallMethodOf(curIndex);
        }

        if(TiltFiveInputs.Instance.stickY < -0.25f && swapTimer <= 0)
        {
            swapTimer = swapTime;
            curIndex++;
            if(curIndex >= allButtons.Length)
            {
                curIndex = 0;
            }
        }
        else if(TiltFiveInputs.Instance.stickY > 0.25f && swapTimer <= 0)
        {
            swapTimer = swapTime;
            curIndex--;
            if(curIndex < 0)
            {
                curIndex = allButtons.Length - 1;
            }
        }
        else if(TiltFiveInputs.Instance.stickX > 0.25f && swapTimer <= 0 && allButtons.Length > 3)
        {
            swapTimer = swapTime;
            curIndex = 3;

        }
        else if (TiltFiveInputs.Instance.stickX < -0.25f && swapTimer <= 0 && allButtons.Length > 3)
        {
            swapTimer = swapTime;
            if (curIndex == 3)
            {
                curIndex = 0;
            }
        }
        HighlightCurButton(curIndex);
        swapTimer -= Time.deltaTime;
    }*/

    public void HighlightCurButton(int index)
    {
        allButtons[index].Select();
    }

    public void HighlightCurButton(Selectable newSel)
    {
        newSel.Select();
    }

    public void CallMethodOf(Selectable newSel) 
    {
        Debug.Log("CallMethodOf called");
        if (swapTimer <= 0)
        {
            Debug.Log("CallMethodOf in if");
            swapTimer = swapTime*10;
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
}
