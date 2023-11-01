using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingObject : InteractableObject
{
    private GameObject thiefModel;
    private bool thiefModelState;
    private ParticleSystem ps;
    public AudioClip appearSound;
    public AudioClip disappearSound;

    private float coolDown = 1;
    float lastInputTimeStamp;

    private void Start()
    {
        thiefModel = GameObject.Find("SM_Chr_Business_Male_01");
        thiefModelState = thiefModel.activeSelf;
        ps = GetComponent<ParticleSystem>();
    }

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

        ps.Play();
        if (Thief.Instance.IsHidden)
        {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(disappearSound);

        } else
        {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(appearSound);
        }
        
        thiefModelState = !thiefModelState;
        thiefModel.SetActive(thiefModelState);
    }
}

