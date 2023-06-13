using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TiltFiveUI : MonoBehaviour
{
    [SerializeField] Selectable[] allButtons;
    public int curIndex = 0;
    public float swapTime = 0.33f;
    float swapTimer = 0;

    private void Update()
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
    }

    public void HighlightCurButton(int index)
    {
        allButtons[index].Select();
    }

    public void CallMethodOf(int index) 
    {
        Debug.Log("CallMethodOf called");
        if (swapTimer <= 0)
        {
            Debug.Log("CallMethodOf in if");
            swapTimer = swapTime*10;
            if (allButtons[index] is Button)
            {
                allButtons[index].GetComponent<Button>().onClick.Invoke();
            }
            else if (allButtons[index] is Toggle)
            {
                allButtons[index].GetComponent<Toggle>().isOn = !allButtons[index].GetComponent<Toggle>().isOn;
            }
        }
    }
}
