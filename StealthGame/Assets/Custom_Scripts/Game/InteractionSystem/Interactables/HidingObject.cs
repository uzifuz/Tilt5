using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingObject : InteractableObject
{
    public AudioClip appearSound;
    public AudioClip disappearSound;

    private float coolDown = 1;
    float lastInputTimeStamp;

    public override void Interact()
    {
        base.Interact();

        if(Time.time - lastInputTimeStamp < coolDown)
        {
            return;
        }
        lastInputTimeStamp = Time.time;
        //ToggleObjectHighlight(true);

        Thief.Instance.IsHidden = !Thief.Instance.IsHidden;
        Thief.Instance.CanMove = !Thief.Instance.IsHidden;
        //Thief.Instance.agent.isStopped = Thief.Instance.IsHidden;
        Thief.Instance.agent.SetDestination(Thief.Instance.transform.position);

        GetComponentInChildren<ParticleSystem>().Play();
        if (Thief.Instance.IsHidden)
        {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(disappearSound);

        } else
        {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(appearSound);
        }

        Thief.Instance.CharacterRenderer.SetActive(!Thief.Instance.CharacterRenderer.activeSelf);
    }

}

