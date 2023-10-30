using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gadget : ScriptableObject
{
    public string Name;
    public Sprite Icon;

    public virtual void UseGadget()
    {
        Debug.Log("Used " + Name);
    }
}


