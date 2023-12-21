using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingObject : InteractableObject
{
    public AudioClip appearSound;
    public AudioClip disappearSound;
    public float ReappearanceDelay = 1f;
    private float coolDown = 1;
    float lastInputTimeStamp;

    protected override void InheritStart()
    {
        base.InheritStart();
        InteractionText.text = "";
    }

    public override void Interact()
    {
        base.Interact();

        if(Time.time - lastInputTimeStamp < coolDown)
        {
            return;
        }
        lastInputTimeStamp = Time.time;

        Thief.Instance.IsHidden = !Thief.Instance.IsHidden;
        Thief.Instance.CanMove = !Thief.Instance.IsHidden;
        Thief.Instance.agent.SetDestination(Thief.Instance.transform.position);

        GetComponentInChildren<ParticleSystem>().Play();
        if (Thief.Instance.IsHidden)
        {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(disappearSound);
            InteractionText.text = "<b>1</b>\nStop Hiding";
            onEvents.Invoke();
            Thief.Instance.CharacterRenderer.SetActive(!Thief.Instance.CharacterRenderer.activeSelf);
        } 
        else
        {
            StartCoroutine(ReappearCo());
        }
    }

    IEnumerator ReappearCo()
    {
        offEvents.Invoke();
        yield return new WaitForSeconds(ReappearanceDelay);
        Camera.main.GetComponent<AudioSource>().PlayOneShot(appearSound);
        InteractionText.text = "<b>1</b>\nHide";
        Thief.Instance.CharacterRenderer.SetActive(!Thief.Instance.CharacterRenderer.activeSelf);
    }

    protected override void SwitchHighlightOn()
    {
        base.SwitchHighlightOn();
        InteractionText.text = "<b>1</b>\nHide";
        InteractionText.gameObject.SetActive(true);
    }

    protected override void SwitchHighlightOff()
    {
        base.SwitchHighlightOff();
        InteractionText.gameObject.SetActive(false);
    }

}

