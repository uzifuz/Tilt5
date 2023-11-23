using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAbility : Ability
{
    public void UseAbility()
    {
        Thief.Instance.WizardAbility();
    }
}
