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
            Noise newNoise = Instantiate(stepNoiseObject, transform.position, transform.rotation).GetComponent<Noise>();
            newNoise.soundIntensity = Mathf.Pow(Thief.Instance.agent.speed, 2) * (100f + PlayerPrefs.GetFloat("NoiseMod")) / 100f;
            newNoise.soundRange = newNoise.soundIntensity + 2f;
        }
    }
}
