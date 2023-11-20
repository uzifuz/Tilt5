using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    Renderer thisRenderer;
    [SerializeField]
    float maxFresnelIntensity = 1f;
    public bool InteractionReady = true;
    public Slider ContextualSlider;
    public TextMeshProUGUI InteractionText;
    public UnityEvent onEvents, offEvents;

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
        if(newState)//Is about to be interacted with
        {
            SwitchHighlightOn();
        }
        else
        {
            SwitchHighlightOff();
        }
    }

    protected virtual void SwitchHighlightOn()
    {

    }

    protected virtual void SwitchHighlightOff()
    {

    }
}
