using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAnimationController : MonoBehaviour
{
    public enum AnimationStates { idle = 0, walk = 1, dead = 2}
    public AnimationStates currentState = AnimationStates.idle;
    bool sneaking = false;
    [SerializeField]
    float stoppingDistance = 0.15f;
    Animator anim;
    public AudioSource footstepSource;
    ControllableEntity assocEntity;
    public GameObject stepNoiseObject;

    private void Start()
    {
        assocEntity = GetComponent<ControllableEntity>();
        anim = GetComponent<Animator>();
    }

    public void GenerateFootstepSound()
    {
        footstepSource.Play();
        if(stepNoiseObject != null)
        {
            Instantiate(stepNoiseObject, transform.position, transform.rotation);
        }
    }

    private void FixedUpdate()
    {
        if(assocEntity.agent.remainingDistance <= stoppingDistance)
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
