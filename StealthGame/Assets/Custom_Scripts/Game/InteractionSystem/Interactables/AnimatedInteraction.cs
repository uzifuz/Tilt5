using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedInteraction : InteractableObject
{
    Animator anim;
    bool on = false;
    [SerializeField] float cooldown = 3f;

    protected override void InheritStart()
    {
        anim = GetComponent<Animator>();
    }

    public override void Interact()
    {
        base.Interact();
        if(InteractionReady)
        {
            on = !on;
            anim.SetBool("on", on);
            InteractionReady = false;
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        InteractionReady = true;
    }
}
