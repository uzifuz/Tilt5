using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingObject : InteractableObject
{
    
    public override void Interact()
    {
        base.Interact();
        //base.ToggleObjectHighlight(true);
        var thief = Thief.Instance;

        thief.IsHidden = !thief.IsHidden;
        thief.CanMove = !thief.IsHidden;
        thief.agent.isStopped = thief.IsHidden;
    }
}
