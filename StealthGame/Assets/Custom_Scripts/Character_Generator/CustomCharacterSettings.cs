using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CustomCharacterSettings : MonoBehaviour
{
    public class ArrayAndIndex
    {
        public IndexWrapper assocIndex = new IndexWrapper();
        public GameObject[] assocArray;
        public ArrayAndIndex(IndexWrapper newIndex, GameObject[] newArray)
        {
            assocIndex = newIndex;
            assocArray = newArray;
        }
    };

    public class IndexWrapper
    {
        public int IntValue = 0;
    };

    public enum CharacterClass { Knight = 0, Thief = 1, Wizard = 2 }
    public CharacterClass CurrentCharacterClass;
    public enum PartToSwap { hair = 0, beard = 1, head = 2, eyeBrows = 3, torso = 4, shoulders = 5, hips = 6, upperArm = 7, ellbow = 8, lowerArm = 9, hands = 10, legs = 11, weapon = 12 }
    public bool male = true;//Female if false
    [SerializeField] GameObject[] hair;
    [SerializeField] GameObject[] beardMale, beardFemale;
    [SerializeField] GameObject[] headMale, headFemale;
    [SerializeField] GameObject[] eyeBrowsMale, eyeBrowsFemale;
    [SerializeField] GameObject[] torsoMale, torsoFemale;
    [SerializeField] GameObject[] hipsMale, hipsFemale;
    [SerializeField] GameObject[] leftShoulder, rightShoulder;
    [SerializeField] GameObject[] leftHandsMale, rightHandsMale, leftHandsFemale, rightHandsFemale;
    [SerializeField] GameObject[] leftLowerArmMale, rightLowerArmMale, leftLowerArmFemale, rightLowerArmFemale;
    [SerializeField] GameObject[] leftEllbow, rightEllbow;
    [SerializeField] GameObject[] leftUpperArmMale, rightUpperArmMale, leftUpperArmFemale, rightUpperArmFemale;
    [SerializeField] GameObject[] leftLegMale, rightLegMale, leftLegFemale, rightLegFemale;
    [SerializeField] GameObject[] weapons;
    float speedModTotal = 0f;
    float damageModTotal = 0f;
    float cooldownModTotal = 0f;
    float noiseModTotal = 0f;
    IndexWrapper hairIndex = new IndexWrapper(), beardIndex = new IndexWrapper(), headIndex = new IndexWrapper(), eyebrowsIndex = new IndexWrapper(), torsoIndex = new IndexWrapper(), shoulderIndex = new IndexWrapper(), hipsIndex = new IndexWrapper(), handsIndex = new IndexWrapper(), lowerArmIndex = new IndexWrapper(), ellbowIndex = new IndexWrapper(), upperArmIndex = new IndexWrapper(), legsIndex = new IndexWrapper(), weaponIndex = new IndexWrapper();
    public Renderer[] AllRenderer;

    private void Start()
    {
        ReadAllIndicesFromPlayerPref();
        SetAllPartsActive();
        AllRenderer = GetAllRenderers();
        InitializeCoupling();
        InitializeAllParts();
        SwapMaleFemale(male);
    }

    void InitializeCoupling()
    {
        CoupleDoubleParts(leftHandsMale, rightHandsMale);
        CoupleDoubleParts(leftHandsFemale, rightHandsFemale);
        CoupleDoubleParts(leftLowerArmMale, rightLowerArmMale);
        CoupleDoubleParts(leftLowerArmFemale, rightLowerArmFemale);
        CoupleDoubleParts(leftUpperArmMale, rightUpperArmMale);
        CoupleDoubleParts(leftUpperArmFemale, rightUpperArmFemale);
        CoupleDoubleParts(leftLegMale, rightLegMale);
        CoupleDoubleParts(leftLegFemale, rightLegFemale);
        CoupleDoubleParts(leftShoulder, rightShoulder);
        CoupleDoubleParts(leftEllbow, rightEllbow);
    }

    void CoupleDoubleParts(GameObject[] newArray, GameObject[] coupledArray)
    {
        for (int i = 0; i < newArray.Length; i++)
        {
            newArray[i].GetComponent<PartSynchronisation>().SetCouplingTo(coupledArray[i]);
        }
    }

    void SetAllOfArrayInactive(GameObject[] newArray)
    {
        for (int i = 0; i < newArray.Length; i++)
        {
            newArray[i].SetActive(false);
        }
    }

    void SetAllOfArrayActive(GameObject[] newArray)
    {
        foreach (GameObject obj in newArray)
        {
            obj.SetActive(true);
        }
    }

    void SetAllPartsActive()
    {
        SetAllOfArrayActive(hair);
        SetAllOfArrayActive(beardMale);
        SetAllOfArrayActive(beardFemale);
        SetAllOfArrayActive(headMale);
        SetAllOfArrayActive(headFemale);
        SetAllOfArrayActive(eyeBrowsMale);
        SetAllOfArrayActive(eyeBrowsFemale);
        SetAllOfArrayActive(torsoMale);
        SetAllOfArrayActive(torsoFemale);
        SetAllOfArrayActive(hipsMale);
        SetAllOfArrayActive(hipsFemale);
        SetAllOfArrayActive(leftUpperArmMale);
        SetAllOfArrayActive(rightUpperArmMale);
        SetAllOfArrayActive(leftLowerArmMale);
        SetAllOfArrayActive(rightLowerArmMale);
        SetAllOfArrayActive(leftHandsMale);
        SetAllOfArrayActive(rightHandsMale);
        SetAllOfArrayActive(leftUpperArmFemale);
        SetAllOfArrayActive(rightUpperArmFemale);
        SetAllOfArrayActive(leftLowerArmFemale);
        SetAllOfArrayActive(rightLowerArmFemale);
        SetAllOfArrayActive(leftHandsFemale);
        SetAllOfArrayActive(rightHandsFemale);
        SetAllOfArrayActive(leftShoulder);
        SetAllOfArrayActive(leftEllbow);
        SetAllOfArrayActive(leftLegMale);
        SetAllOfArrayActive(rightLegMale);
        SetAllOfArrayActive(leftLegFemale);
        SetAllOfArrayActive(leftLegMale);
        SetAllOfArrayActive(weapons);
    }

    ArrayAndIndex GetArrayByPart(PartToSwap thisPart)
    {
        switch(thisPart)
        {
            case PartToSwap.hair:
                return new ArrayAndIndex(hairIndex, hair);
            case PartToSwap.beard:
                return new ArrayAndIndex(beardIndex, male ? beardMale : beardFemale);
            case PartToSwap.head:
                return new ArrayAndIndex(headIndex, male ? headMale : headFemale);
            case PartToSwap.eyeBrows:
                return new ArrayAndIndex(eyebrowsIndex, male ? eyeBrowsMale : eyeBrowsFemale);
            case PartToSwap.torso:
                return new ArrayAndIndex(torsoIndex, male ? torsoMale : torsoFemale);
            case PartToSwap.shoulders:
                return new ArrayAndIndex(shoulderIndex, leftShoulder);
            case PartToSwap.hips:
                return new ArrayAndIndex(hipsIndex, male ? hipsMale : hipsFemale);
            case PartToSwap.upperArm:
                return new ArrayAndIndex(upperArmIndex, male ? leftUpperArmMale : leftUpperArmFemale);
            case PartToSwap.ellbow:
                return new ArrayAndIndex(ellbowIndex, leftEllbow);
            case PartToSwap.lowerArm:
                return new ArrayAndIndex(lowerArmIndex, male ? leftLowerArmMale : leftLowerArmFemale);
            case PartToSwap.hands:
                return new ArrayAndIndex(handsIndex, male ? leftHandsMale : leftHandsFemale);
            case PartToSwap.legs:
                return new ArrayAndIndex(legsIndex, male ? leftLegMale : leftLegFemale);
            case PartToSwap.weapon:
                return new ArrayAndIndex(weaponIndex, weapons);

        }
        return new ArrayAndIndex(null, null);
    }

    public void SwapMaleFemale(bool newValue)
    {
        male = newValue;
        switch(male)
        {
            case true:
                SetAllOfArrayInactive(headFemale);
                SetAllOfArrayInactive(beardFemale);
                SetAllOfArrayInactive(eyeBrowsFemale);
                SetAllOfArrayInactive(torsoFemale);
                SetAllOfArrayInactive(hipsFemale);
                SetAllOfArrayInactive(leftUpperArmFemale);
                SetAllOfArrayInactive(leftLowerArmFemale);
                SetAllOfArrayInactive(leftHandsFemale);
                SetAllOfArrayInactive(leftLegFemale);

                break;
            case false:
                SetAllOfArrayInactive(headMale);
                SetAllOfArrayInactive(beardMale);
                SetAllOfArrayInactive(eyeBrowsMale);
                SetAllOfArrayInactive(torsoMale);
                SetAllOfArrayInactive(hipsMale);
                SetAllOfArrayInactive(leftUpperArmMale);
                SetAllOfArrayInactive(leftLowerArmMale);
                SetAllOfArrayInactive(leftHandsMale);
                SetAllOfArrayInactive(leftLegMale);

                break;
        }

        for (int i = 0; i < Enum.GetNames(typeof(PartToSwap)).Length; i++)
        {
            InitializePart(i);
        }
    }

    public void SwapCharacterClass(int dir)
    {
        CurrentCharacterClass += dir;
        if((int)CurrentCharacterClass >= Enum.GetNames(typeof(CharacterClass)).Length)
        {
            CurrentCharacterClass = (CharacterClass)0;
        }
        else if((int)CurrentCharacterClass < 0)
        {
            CurrentCharacterClass = (CharacterClass)Enum.GetNames(typeof(CharacterClass)).Length - 1;
        }
    }

    public GameObject GetCurrentPart(int part)
    {
        ArrayAndIndex newStruct = GetArrayByPart((PartToSwap)part);
        GameObject[] currentArray = newStruct.assocArray;
        IndexWrapper newIndex = newStruct.assocIndex;
        //-->Setting!
        SetAllOfArrayInactive(currentArray);
        currentArray[newIndex.IntValue].SetActive(true);
        return currentArray[newIndex.IntValue];
    }

    public GameObject SwapPartUp(int part)
    {
        ArrayAndIndex newStruct = GetArrayByPart((PartToSwap)part);
        GameObject[] currentArray = newStruct.assocArray;
        IndexWrapper newIndex = newStruct.assocIndex;
        //-->Setting!
        SetAllOfArrayInactive(currentArray);
        do
        {
            newIndex.IntValue = newIndex.IntValue + 1;
            if (newIndex.IntValue >= currentArray.Length)
            {
                newIndex.IntValue = 0;
            }
        }
        while (!currentArray[newIndex.IntValue].GetComponent<PartSpecification>().CheckExclusivity(CurrentCharacterClass));
        currentArray[newIndex.IntValue].SetActive(true);
        return currentArray[newIndex.IntValue];
    }

    public GameObject SwapPartDown(int part)
    {
        ArrayAndIndex newStruct = GetArrayByPart((PartToSwap)part);
        GameObject[] currentArray = newStruct.assocArray;
        IndexWrapper newIndex = newStruct.assocIndex;
        //-->Setting!
        SetAllOfArrayInactive(currentArray);
        do
        {
            newIndex.IntValue = newIndex.IntValue - 1;
            if (newIndex.IntValue < 0)
            {
                newIndex.IntValue = currentArray.Length - 1;
            }
        }
        while (!currentArray[newIndex.IntValue].GetComponent<PartSpecification>().CheckExclusivity(CurrentCharacterClass));
        currentArray[newIndex.IntValue].SetActive(true);
        return currentArray[newIndex.IntValue];
    }

    void InitializeAllParts()
    {
        for (int i = 0; i < Enum.GetNames(typeof(PartToSwap)).Length; i++)
        {
            InitializePart(i);
        }
        male = !male;
        for (int i = 0; i < Enum.GetNames(typeof(PartToSwap)).Length; i++)
        {
            InitializePart(i);
        }
        male = !male;
    }

    void InitializePart(int part)
    {
        ArrayAndIndex newStruct = GetArrayByPart((PartToSwap)part);
        GameObject[] currentArray = newStruct.assocArray;
        SetAllOfArrayInactive(currentArray);
        if (newStruct.assocIndex.IntValue >= currentArray.Length)
            newStruct.assocIndex.IntValue = 0;
        currentArray[newStruct.assocIndex.IntValue].SetActive(true);
    }

    public void SetAllIndicesToPlayerPref()
    {
        PlayerPrefs.SetInt("playerClass", (int)CurrentCharacterClass);
        PlayerPrefs.SetInt("male", male ? 1 : 0);
        PlayerPrefs.SetInt("hairIndex", hairIndex.IntValue);
        PlayerPrefs.SetInt("beardIndex", beardIndex.IntValue);
        PlayerPrefs.SetInt("headIndex", headIndex.IntValue);
        PlayerPrefs.SetInt("eyebrowsIndex", eyebrowsIndex.IntValue);
        PlayerPrefs.SetInt("torsoIndex", torsoIndex.IntValue);
        PlayerPrefs.SetInt("shoulderIndex", shoulderIndex.IntValue);
        PlayerPrefs.SetInt("hipsIndex", hipsIndex.IntValue);
        PlayerPrefs.SetInt("handsIndex", handsIndex.IntValue);
        PlayerPrefs.SetInt("lowerArmIndex", lowerArmIndex.IntValue);
        PlayerPrefs.SetInt("ellbowIndex", ellbowIndex.IntValue);
        PlayerPrefs.SetInt("upperArmIndex", upperArmIndex.IntValue);
        PlayerPrefs.SetInt("legsIndex", legsIndex.IntValue);
        PlayerPrefs.SetInt("weaponIndex", weaponIndex.IntValue);
    }

    public void ReadAllIndicesFromPlayerPref()
    {
        PlayerPrefs.GetInt("playerClass", 0);
        male = PlayerPrefs.GetInt("male", 1) == 1;
        hairIndex.IntValue = PlayerPrefs.GetInt("hairIndex", 0);
        beardIndex.IntValue =  PlayerPrefs.GetInt("beardIndex", 0);
        headIndex.IntValue = PlayerPrefs.GetInt("headIndex", 0);
        eyebrowsIndex.IntValue = PlayerPrefs.GetInt("eyebrowsIndex", 0);
        torsoIndex.IntValue = PlayerPrefs.GetInt("torsoIndex", 0);
        shoulderIndex.IntValue = PlayerPrefs.GetInt("shoulderIndex", 0);
        hipsIndex.IntValue = PlayerPrefs.GetInt("hipsIndex", 0);
        handsIndex.IntValue = PlayerPrefs.GetInt("handsIndex", 0);
        lowerArmIndex.IntValue = PlayerPrefs.GetInt("lowerArmIndex", 0);
        ellbowIndex.IntValue = PlayerPrefs.GetInt("ellbowIndex", 0);
        upperArmIndex.IntValue = PlayerPrefs.GetInt("upperArmIndex", 0);
        legsIndex.IntValue = PlayerPrefs.GetInt("legsIndex", 0);
        weaponIndex.IntValue = PlayerPrefs.GetInt("weaponIndex", 0);
    }

    public Vector4 GetModificationsOfParts()
    {
        speedModTotal = 0f;
        damageModTotal = 0f;
        cooldownModTotal = 0f;
        noiseModTotal = 0f;
        List<PartSpecification> allCurrentParts = new List<PartSpecification>();
        allCurrentParts.Add(hair[hairIndex.IntValue].GetComponentInParent<PartSpecification>());
        allCurrentParts.Add(leftShoulder[shoulderIndex.IntValue].GetComponentInParent<PartSpecification>());
        allCurrentParts.Add(leftEllbow[ellbowIndex.IntValue].GetComponentInParent<PartSpecification>());

        allCurrentParts.Add(torsoMale[torsoIndex.IntValue].GetComponentInParent<PartSpecification>());
        allCurrentParts.Add(hipsMale[hipsIndex.IntValue].GetComponentInParent<PartSpecification>());
        allCurrentParts.Add(leftUpperArmMale[upperArmIndex.IntValue].GetComponentInParent<PartSpecification>());
        allCurrentParts.Add(leftLowerArmMale[lowerArmIndex.IntValue].GetComponentInParent<PartSpecification>());
        allCurrentParts.Add(leftHandsMale[handsIndex.IntValue].GetComponentInParent<PartSpecification>());
        allCurrentParts.Add(leftLegMale[legsIndex.IntValue].GetComponentInParent<PartSpecification>());
        
        allCurrentParts.Add(torsoFemale[torsoIndex.IntValue].GetComponentInParent<PartSpecification>());
        allCurrentParts.Add(hipsFemale[hipsIndex.IntValue].GetComponentInParent<PartSpecification>());
        allCurrentParts.Add(leftUpperArmFemale[upperArmIndex.IntValue].GetComponentInParent<PartSpecification>());
        allCurrentParts.Add(leftLowerArmFemale[lowerArmIndex.IntValue].GetComponentInParent<PartSpecification>());
        allCurrentParts.Add(leftHandsFemale[handsIndex.IntValue].GetComponentInParent<PartSpecification>());
        allCurrentParts.Add(leftLegFemale[legsIndex.IntValue].GetComponentInParent<PartSpecification>());

        Debug.Log(allCurrentParts.Count);

        foreach(PartSpecification part in allCurrentParts)
        {
            if(part != null && part.gameObject.activeSelf)
            {
                speedModTotal += part.SpeedModPercent;
                damageModTotal += part.DamageTakenPercent;
                cooldownModTotal += part.CooldownPercent;
                noiseModTotal += part.NoiseModifierPercent;
            }
        }
        allCurrentParts.Clear();
        return new Vector4(speedModTotal, damageModTotal, cooldownModTotal, noiseModTotal);
    }

    public Renderer[] GetAllRenderers()
    {
        return GetComponentsInChildren<Renderer>();
    }
}
