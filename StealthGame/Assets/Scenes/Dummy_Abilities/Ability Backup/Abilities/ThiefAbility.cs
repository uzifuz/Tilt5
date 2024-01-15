using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class ThiefAbility : Ability
{
    public void UseAbility()
    {
        Cooldown = 5f;
        if (AbilityReady && !Thief.Instance.IsHidden)
        {
            Debug.Log("Used Invis Potion");
            Task.Run(() => EndOfInvisibility());
        }
        else if(Thief.Instance.IsHidden)
        {
            StartCoolDown();
            Thief.Instance.ThiefAbility();
        }
    }

    async Task EndOfInvisibility()
    {
        Thief.Instance.ThiefAbility();
        await Task.Delay(TimeSpan.FromSeconds(Cooldown));
        Debug.Log("Invis active: " + Thief.Instance.IsHidden);
        if (Thief.Instance.IsHidden)
        {
            Thief.Instance.ThiefAbility();
        }
    }
}
