using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPointInteractable : InteractableObject
{
    [SerializeField] GameObject highlightedObject, nonHighlightedObject;
    bool calledOnce = false;

    public void ShowExitHighlight(bool show = false)
    {
        highlightedObject.SetActive(show);
        nonHighlightedObject.SetActive(!show);
    }

    public override void Interact()
    {
        base.Interact();

        if(/*CollectibleMaster.Instance.allCollectiblesFound &&*/ !calledOnce)
        {
            calledOnce = true;
            //Debug.Log($"{GameHandler.Instance != null} {Thief.Instance != null} {DetectionHandler.Instance != null} {MenuHandler.Instance != null}");
            GameHandler.Instance.GameOver(GameHandler.GameOutcome.ThiefWin);
            //Debug.Log("This has been called" + PlayerPrefs.GetInt("TotalPlayerMoney").ToString());
            PlayerPrefs.SetInt("TotalPlayerMoney", PlayerPrefs.GetInt("TotalPlayerMoney") + CollectibleMaster.Instance.collectedValue);
        }

    }
}
