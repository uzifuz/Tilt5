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
        Thief.IsHidden = !Thief.IsHidden;
        Thief.CanMove = !Thief.IsHidden;
        Thief.Instance.agent.isStopped = Thief.IsHidden;
    }
}
