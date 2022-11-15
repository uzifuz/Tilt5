using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControllableEntity : Entity
{
    public bool CanMove;
    public NavMeshAgent agent;
    [SerializeField]
    LayerMask walkableSurfaces;

    protected override void InheritStart()
    {
        base.InheritStart();
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetAgentDestination(Vector3 target)
    {
        if (!CanMove)
        {
            Debug.Log(gameObject.name + ": CanMove is set to false");
            return;
        }

        Vector3 proxyTarget = NavMeshInfo.RandomNavSphere(target, 0, 1, walkableSurfaces);
        agent.SetDestination(proxyTarget);
    }
}
