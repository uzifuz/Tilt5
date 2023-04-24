using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TiltFiveUI : MonoBehaviour
{
    [SerializeField] Button[] allButtons;
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
        HighlightCurButton(curIndex);
        swapTimer -= Time.deltaTime;
    }

    public void HighlightCurButton(int index)
    {
        allButtons[index].Select();
    }

    public void CallMethodOf(int index)
    {
        allButtons[index].onClick.Invoke();
    }
}
