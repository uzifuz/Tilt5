using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartSynchronisation : MonoBehaviour
{
    [SerializeField] GameObject coupledObject;

    public void SetCouplingTo(GameObject newCouple)
    {
        coupledObject = newCouple;
    }

    private void OnEnable()
    {
        if(coupledObject != null )
        {
            coupledObject?.SetActive(true);
        }
    }

    private void OnDisable()
    {
        if(coupledObject != null)
        {
            coupledObject?.SetActive(false);
        }
    }
}
