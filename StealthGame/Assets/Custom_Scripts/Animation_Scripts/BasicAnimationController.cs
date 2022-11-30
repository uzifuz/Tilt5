using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAnimationController : MonoBehaviour
{
    public enum AnimationStates { idle = 0, walk = 1, dead = 2}
    public AnimationStates currentState = AnimationStates.idle;
    bool sneaking = false;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if(Thief.Instance.agent.remainingDistance <= 0.5f)
        {
            currentState = AnimationStates.idle;
        }
        else
        {
            currentState = AnimationStates.walk;
        }
        anim.SetInteger("walkState", (int)currentState);
    }


}
