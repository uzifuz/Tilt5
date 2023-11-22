using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BasicAnimationController : MonoBehaviour
{
    public enum AnimationStates { idle = 0, walk = 1, run = 2, dead = 2}
    public AnimationStates currentState = AnimationStates.idle;
    bool sneaking = false;
    [SerializeField]
    float stoppingDistance = 0.15f;
    Animator anim;
    ControllableEntity assocEntity;

    private void Start()
    {
        assocEntity = GetComponent<ControllableEntity>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if(assocEntity.agent.enabled)
        {
            if(assocEntity.agent.remainingDistance <= stoppingDistance)
            {
                currentState = AnimationStates.idle;
            }
            else if(assocEntity.agent.speed < 2)
            {
                currentState = AnimationStates.walk;
            }
            else
            {
                currentState = AnimationStates.run;
            }
        }

        anim.SetInteger("walkState", (int)currentState);
        anim.SetFloat("moveSpeed", (float)currentState);
    }


}
