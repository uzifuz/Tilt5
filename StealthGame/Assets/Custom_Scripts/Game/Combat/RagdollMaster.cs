using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollMaster : MonoBehaviour
{
    //TODO: Everything!!!
    [SerializeField] Rigidbody hipBody;
    [SerializeField] float forceMod = 1500f;
    Rigidbody[] allRigidbodies;

    private void Start()
    {
        allRigidbodies = GetComponentsInChildren<Rigidbody>();
        SetActiveStateOfRagdollParts(true);
    }

    public void StartRagdolling(Vector3 direction, float force)
    {
        SetActiveStateOfRagdollParts(false);
        hipBody.AddForce(direction.normalized * force * forceMod);
    }

    public void SetActiveStateOfRagdollParts(bool kinematic)
    {
        foreach (var rigidbody in allRigidbodies)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.isKinematic = kinematic;
            rigidbody.useGravity = !kinematic;
            rigidbody.GetComponent<Collider>().enabled = !kinematic;
        }
    }
}
