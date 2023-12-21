using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefAbility : Ability
{
    public void UseAbility(float invisibilityTime = 5f)
    {
        Cooldown = 5f;
        if (AbilityReady && !Thief.Instance.IsHidden)
        {
            StayInvisibleCo();
            Thief.Instance.ThiefAbility();
        }
        else if(Thief.Instance.IsHidden)
        {
            
        }
    }

    IEnumerator StayInvisibleCo()
    {
        yield return new WaitForSeconds(5f);
        //If still invisible, get visible again
        Thief.Instance.ThiefAbility();
        StartCoolDown();
    }
}
