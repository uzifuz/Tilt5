using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepNoiseGenerator : MonoBehaviour
{
    public GameObject stepNoiseObject;
    private AudioSource footstepSource;

    void Start()
    {
        footstepSource = GetComponentInChildren<AudioSource>();
    }

    public void GenerateFootstepSound()
    {
        if (stepNoiseObject != null && !Thief.Instance.IsHidden)
        {
            footstepSource.Play();
            Instantiate(stepNoiseObject, transform.position, transform.rotation);
        }
    }
}
