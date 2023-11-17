
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : ControllableEntity
{
    public static Thief Instance { get; private set; }

    public GameObject CharacterRenderer;
    public bool IsHidden
    {
        get;
        set;
    }
    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Debug.LogWarning("more than two tieves detected!");
            gameObject.SetActive(false);
        }
        IsHidden = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            AbilityController.Instance.UseAbility();
        }
    }

    public void KnightAbility()
    {
        anim.SetFloat("animSpeed", 1);
        anim.Play("Ability");
    }

    public void ThiefAbility()
    {

    }

    public void WizardAbility()
    {

    }
}
