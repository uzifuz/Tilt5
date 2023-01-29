using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPointInteractable : InteractableObject
{
    public override void Interact()
    {
        base.Interact();

        if(CollectibleMaster.Instance.uiCounter.winCondition)
        {
            GameHandler.Instance.GameOver(GameHandler.GameOutcome.ThiefWin);
        }

    }
}
