using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterGeneration : MonoBehaviour
{
    public int CurrentPartSelection = 0;
    [SerializeField] CustomCharacterSettings charCreator;
    [SerializeField] TextMeshProUGUI partText, itemNameText;

    public void SwapPart(int dir)
    {
        GameObject curPart = null;
        switch(dir)
        {
            case -1: 
                curPart = charCreator.SwapPartDown(CurrentPartSelection); 
                break;
            case 1: 
                curPart = charCreator.SwapPartUp(CurrentPartSelection); 
                break;
        }
        SetUITexts(curPart.GetComponent<PartSpecification>());
    }

    void SetUITexts(PartSpecification newPart)
    {
        string partString = "";
        switch(CurrentPartSelection)
        {
            case 0: partString = "--Head--";
                break;
            case 1: partString = "--Beard--";
                break;
            case 2: partString = "--Face--";
                break;
            case 3: partString = "--Eyebrows--";
                break;
            case 4: partString = "--Torso--";
                break;
            case 5: partString = "--Shoulders--";
                break;
            case 6: partString = "--Hips--";
                break;
            case 7: partString = "--Arms (Upper)--";
                break;
            case 8:partString = "--Ellbow--";
                break;
            case 9: partString = "--Arms (Lower)--";
                break;
            case 10: partString = "--Hands--";
                break;
            case 11: partString = "--Legs--";
                break;
        }
        partText.text = partString;
        itemNameText.text = newPart.PartName;
    }

    public void SwitchGender()
    {
        charCreator.SwapMaleFemale(!charCreator.male);
    }

    public void ConfirmSelection()
    {
        charCreator.SetAllIndicesToPlayerPref();
    }
}
