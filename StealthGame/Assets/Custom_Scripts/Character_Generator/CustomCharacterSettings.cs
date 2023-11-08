using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCharacterSettings : MonoBehaviour
{
    bool male = true;//Female if false
    [SerializeField] GameObject[] headMale, headFemale;
    [SerializeField] GameObject[] torsoMale, torsoFemale;
    [SerializeField] GameObject[] hipsMale, hipsFemale;
    [SerializeField] GameObject[] leftHands, rightHands;
    [SerializeField] GameObject[] leftLowerArm, rightLowerArm;
    [SerializeField] GameObject[] leftUpperArm, rightUpperArm;
    int headIndex = 0, torsoIndex = 0, hipsIndex = 0, handsIndex = 0, lowerArmIndex = 0, upperArmIndex = 0;

    private void Start()
    {
        SwapHead(0);
    }

    void SetAllOfArrayInactive(GameObject[] newArray)
    {
        foreach(GameObject obj in newArray)
        {
            obj.SetActive(false);
        }
    }

    public void SwapHead(int direction)
    {
        SetAllOfArrayInactive(headMale);
        SetAllOfArrayInactive(headFemale);
        headIndex = headIndex + direction;
        if (male)
        {
            if(headIndex >=  headMale.Length)
            {
                headIndex = 0;
            }
            else if(headIndex < 0)
            {
                headIndex = headMale.Length - 1;
            }
            headMale[headIndex].SetActive(true);
        }
        else
        {
            if (headIndex >= headFemale.Length)
            {
                headIndex = 0;
            }
            else if (headIndex < 0)
            {
                headIndex = headFemale.Length - 1;
            }
            headFemale[headIndex].SetActive(true);
        }
    }

    public void SwapTorso(int direction)
    {
        torsoIndex = torsoIndex + direction;
        if (male)
        {
            if (torsoIndex > torsoMale.Length)
            {
                headIndex = 0;
            }
        }
        else
        {
            if (torsoIndex > torsoFemale.Length)
            {
                headIndex = 0;
            }
        }
    }
}
