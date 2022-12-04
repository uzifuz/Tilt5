using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TMPro.EditorUtilities;
using static UnityEngine.Rendering.DebugUI;

public class ExitPointInteractable : InteractableObject
{

    public override void Interact()
    {
        base.Interact();

        if(CollectibleCount.winCondition)
        {
            GameHandler.Instance.GameOver(GameHandler.GameOutcome.ThiefWin);
        }

    }
}
