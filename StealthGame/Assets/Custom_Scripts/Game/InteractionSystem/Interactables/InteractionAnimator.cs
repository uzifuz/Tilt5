using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionAnimator : MonoBehaviour
{
    [SerializeField] Animator anim;

    public void PlayAnimation(string animationName)
    {
        anim.Play(animationName, 0);
    }
}
