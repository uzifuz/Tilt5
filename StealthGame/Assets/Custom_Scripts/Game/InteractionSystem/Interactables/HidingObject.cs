using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingObject : InteractableObject
{
    private GameObject thiefModel;
    private bool thiefModelState;

    private void Start()
    {
        thiefModel = GameObject.Find("SM_Chr_Business_Male_01");
        thiefModelState = thiefModel.activeSelf;
    }

    public override void Interact()
    {
        base.Interact();
        //ToggleObjectHighlight(true);

        Thief.Instance.IsHidden = !Thief.Instance.IsHidden;
        Thief.Instance.CanMove = !Thief.Instance.IsHidden;
        //Thief.Instance.agent.isStopped = Thief.Instance.IsHidden;
        Thief.Instance.agent.SetDestination(Thief.Instance.transform.position);
        
        thiefModelState = !thiefModelState;
        thiefModel.SetActive(thiefModelState);
    }
}

