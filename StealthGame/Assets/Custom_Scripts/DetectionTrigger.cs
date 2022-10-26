using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var thief = other.GetComponent<Thief>();
        if (thief != null)
        {
            DetectionHandler.ThiefDetected = true;
        }
    }
}
