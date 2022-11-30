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
            modIntensity = soundIntensity / coll.radius;
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
            Debug.Log("Guard heared a noise");
            if(modIntensity > other.GetComponent<Guard>().hearingSensitivity)
            {
                Debug.Log("Noise was loud enough");
                other.GetComponent<Guard>().SetAgentDestination(transform.position, false, 1f);
            }
            else
            {
                Debug.Log("Noise was too quiet");
            }
        }
    }
}
