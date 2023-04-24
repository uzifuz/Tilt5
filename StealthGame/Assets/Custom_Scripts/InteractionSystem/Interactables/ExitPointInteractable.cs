using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPointInteractable : InteractableObject
{
    [SerializeField] GameObject highlightedObject, nonHighlightedObject;

    public void ShowExitHighlight(bool show = false)
    {
        highlightedObject.SetActive(show);
        nonHighlightedObject.SetActive(!show);
    }

    public override void Interact()
    {
        base.Interact();

        if(CollectibleMaster.Instance.uiCounter.winCondition)
        {
            GameHandler.Instance.GameOver(GameHandler.GameOutcome.ThiefWin);
        }

    }
}
