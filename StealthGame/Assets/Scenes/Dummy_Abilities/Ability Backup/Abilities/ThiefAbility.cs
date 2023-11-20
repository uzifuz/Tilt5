using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefAbility : Ability
{
    public void UseAbility()
    {
        Thief.Instance.ThiefAbility();
    }
}
