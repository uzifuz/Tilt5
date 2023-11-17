using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Ability
{
    public string Name;
    public bool AbilityReady = true;
    public float Cooldown = 2f;

    public void StartCoolDown()
    {
        Task.Run(() => CooldownCo());
    }

    async Task CooldownCo()
    {
        Debug.Log("Cooldown started");
        AbilityReady = false;
        await Task.Delay(TimeSpan.FromSeconds(Cooldown));
        AbilityReady = true;
        Debug.Log("Cooldown ended");
    }

}


