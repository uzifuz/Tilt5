
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

    private Animator animator;

    private AbilityController abilityController;

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
        animator = GetComponent<Animator>();
        abilityController = GetComponent<AbilityController>();

        abilityController.SetAbilitySlotUI(CharacterClass);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
