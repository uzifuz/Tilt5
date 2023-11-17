using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    Renderer thisRenderer;
    [SerializeField]
    float maxFresnelIntensity = 1f;
    public bool InteractionReady = true;

    private void Start()
    {
        InheritStart();
        ToggleObjectHighlight(false);
    }

    public virtual void Interact()
    {
        Debug.Log($"Interacted with {name}");
    }

    private void Update()
    {
        InheritUpdate();
    }

    protected virtual void InheritStart()
    {

    }

    protected virtual void InheritUpdate()
    {

    }

    public void ToggleObjectHighlight(bool newState)
    {
        thisRenderer = GetComponent<Renderer>();
        if(newState)
        {
            //thisRenderer?.material.SetFloat("_FresnelIntensity", maxFresnelIntensity);
        }
        else
        {
            //thisRenderer?.material.SetFloat("_FresnelIntensity", 0);
        }
    }
}
