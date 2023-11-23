using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterGeneration : MonoBehaviour
{
    /*
     * hair = 0, 
     * beard = 1, 
     * head = 2, 
     * eyeBrows = 3, 
     * torso = 4, 
     * shoulders = 5, 
     * hips = 6, 
     * upperArm = 7, 
     * ellbow = 8, 
     * lowerArm = 9, 
     * hands = 10, 
     * legs = 11, 
     * weapon = 12 
    */
    [SerializeField] CustomCharacterSettings charCreator;
    [SerializeField] CharacterCreationPanel classPanel;
    [SerializeField] CharacterCreationPanel headPanel;
    [SerializeField] CharacterCreationPanel beardPanel;
    [SerializeField] CharacterCreationPanel facePanel;
    [SerializeField] CharacterCreationPanel eyebrowsPanel;
    [SerializeField] CharacterCreationPanel torsoPanel;
    [SerializeField] CharacterCreationPanel shouldersPanel;
    [SerializeField] CharacterCreationPanel hipsPanel;
    [SerializeField] CharacterCreationPanel upperArmPanel;
    [SerializeField] CharacterCreationPanel ellbowPanel;
    [SerializeField] CharacterCreationPanel lowerArmPanel;
    [SerializeField] CharacterCreationPanel handsPanel;
    [SerializeField] CharacterCreationPanel legsPanel;
    [SerializeField] CharacterCreationPanel weaponsPanel;
    [SerializeField] CharacterCreationPanel speedModPanel;
    [SerializeField] CharacterCreationPanel damageModPanel;
    [SerializeField] CharacterCreationPanel cooldownModPanel;
    [SerializeField] CharacterCreationPanel noiseModPanel;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        InitializeUI();
    }

    private void FixedUpdate()
    {
        anim.SetInteger("Class", (int)charCreator.CurrentCharacterClass);
    }

    void InitializeUI()
    {
        SwapClass(0);
        for (int i = 0; i < 13; i++)
        {
            SetUITexts(charCreator.GetCurrentPart(i).GetComponent<PartSpecification>(), i);
        }
    }

    public void SwapClass(int dir)
    {
        charCreator.SwapCharacterClass(dir);
        SetUITexts(null, -1);
    }

    public void SwapPartUp(int partNumber)
    {
        GameObject curPart = null;
        curPart = charCreator.SwapPartUp(partNumber);
        SetUITexts(curPart.GetComponent<PartSpecification>(), partNumber);
    }

    public void SwapPartDown(int partNumber)
    {
        GameObject curPart = null;
        curPart = charCreator.SwapPartDown(partNumber);
        SetUITexts(curPart.GetComponent<PartSpecification>(), partNumber);
    }

    void SetUITexts(PartSpecification newPart, int partNr)
    {
        CharacterCreationPanel newPanel = null;

        string partString = "";
        string partName = "";
        switch(partNr)
        {
            case -1:
                partString = "--Class--";
                newPanel = classPanel;
                break;
            case 0: partString = "--Head--";
                newPanel = headPanel;
                break;
            case 1: partString = "--Beard--";
                newPanel = beardPanel;
                break;
            case 2: partString = "--Face--";
                newPanel = facePanel;
                break;
            case 3: partString = "--Eyebrows--";
                newPanel = eyebrowsPanel;
                break;
            case 4: partString = "--Torso--";
                newPanel = torsoPanel;
                break;
            case 5: partString = "--Shoulders--";
                newPanel = shouldersPanel;
                break;
            case 6: partString = "--Hips--";
                newPanel = hipsPanel;
                break;
            case 7: partString = "--Arms (Upper)--";
                newPanel = upperArmPanel;
                break;
            case 8:partString = "--Ellbow--";
                newPanel = ellbowPanel;
                break;
            case 9: partString = "--Arms (Lower)--";
                newPanel = lowerArmPanel;
                break;
            case 10: partString = "--Hands--";
                newPanel = handsPanel;
                break;
            case 11: partString = "--Legs--";
                newPanel = legsPanel;
                break;
            case 12:
                partString = "--Equipment--";
                newPanel = weaponsPanel;
                break;
        }
        if (newPart == null)
            partName = charCreator.CurrentCharacterClass.ToString();
        else
            partName = newPart.PartName;
        newPanel.UpdateTexts(partString, partName);
        SetModUI();
    }

    public void SetModUI()
    {
        Vector4 mods = charCreator.GetModificationsOfParts();
        string compositeString = (100f + mods.x) + "% (" + (mods.x >= 0 ? "<color=green>" : "<color=red>") + mods.x.ToString("0.0") + "%</color>)";
        speedModPanel.UpdateTexts("Movement Speed", compositeString);

        compositeString = (100f + mods.y) + "% (" + (mods.y <= 0 ? "<color=green>" : "<color=red>") + mods.y.ToString("0.0") + "%</color>)";
        damageModPanel.UpdateTexts("Damage Taken", compositeString);

        compositeString = (100f + mods.z) + "% (" + (mods.z <= 0 ? "<color=green>" : "<color=red>") + mods.z.ToString("0.0") + "%</color>)";
        cooldownModPanel.UpdateTexts("Cooldown", compositeString);

        compositeString = (100f + mods.w) + "% (" + (mods.w <= 0 ? "<color=green>" : "<color=red>") + mods.w.ToString("0.0") + "%</color>)";
        noiseModPanel.UpdateTexts("Step Noise Volume", compositeString);
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
