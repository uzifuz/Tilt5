
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterClass
{
    Knight = 0,
    Thief = 1,
    Mage = 2,
}

public class Thief : ControllableEntity
{
    public static Thief Instance { get; private set; }
    public bool IsHidden
    {
        get;
        set;
    }

    [SerializeField]
    public CharacterClass CharacterClass;

    [SerializeField]
    public AbilitySlot AbilitySlot;


    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Debug.LogWarning("more than one thief detected!");
            gameObject.SetActive(false);
        }
        IsHidden = false;

        switch(CharacterClass)
        {
            case CharacterClass.Knight:
                AbilitySlot.Ability = AbilitySlot.Abilities[0];
                break;
            case CharacterClass.Thief:
                AbilitySlot.Ability = AbilitySlot.Abilities[1];
                break;
            case CharacterClass.Mage:
                AbilitySlot.Ability = AbilitySlot.Abilities[2];
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameHandler.Instance.GameIsOver)
        {
            if(Input.GetKeyDown(KeyCode.G))
            {
                if (AbilitySlot.Ability != null)
                {
                    AbilitySlot.Ability.UseAbility();
                }
                else
                {
                    Debug.Log("No ability instantiated");
                }
            }
        }
    }

}
