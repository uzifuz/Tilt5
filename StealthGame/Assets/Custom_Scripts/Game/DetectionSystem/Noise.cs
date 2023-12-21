using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise : MonoBehaviour
{
    public float soundRange = 1f, soundIntensity = 1f;
    private float modIntensity;
    SphereCollider coll;

    private void Start()
    {
        coll = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(coll.radius < soundRange)
        {
            coll.radius += Time.deltaTime * 15f;
            modIntensity = soundIntensity / Mathf.Log10(coll.radius);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Guard>() != null)
        {
            Guard thisGuard = other.GetComponent<Guard>();
            if(modIntensity > thisGuard.hearingSensitivity)
            {
                //Guard heard the noise
                thisGuard.suspicionLevel += modIntensity;
                if(Random.Range(25f, 100f) < thisGuard.suspicionLevel)
                {
                    thisGuard.SetAgentDestination(transform.position, false, 1f);
                }
            }
            else
            {
                //Noise was too quiet
            }
        }
    }
}
