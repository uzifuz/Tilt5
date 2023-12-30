using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwing : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Guard"))
        {
            
            Debug.Log("Collision with GUard");
            ControllableEntity guard = other.gameObject.GetComponent<ControllableEntity>();
            Vector3 hitVector = other.transform.position - Thief.Instance.transform.position;
            if (!guard.ModifyHealth(-100f))
            {
                guard.Death(hitVector.normalized *Random.Range(5f, 10f)); 
            }
            Debug.LogWarning("Did a damage on " + guard.name);
        }
    }
}
