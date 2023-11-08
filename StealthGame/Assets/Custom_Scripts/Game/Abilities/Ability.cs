using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public double Cooldown = 10.0f;

    public virtual void UseAbility()
    {
        Debug.Log("Used " + Name);
    }
}


