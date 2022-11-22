using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Thief>()
            && !Thief.IsHidden)
        {
            DetectionHandler.ThiefDetected = true;
        }
    }
}
