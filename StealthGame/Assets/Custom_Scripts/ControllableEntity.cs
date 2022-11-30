using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControllableEntity : Entity
{
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

        Vector3 proxyTarget = NavMeshInfo.RandomNavSphere(target, 0, 1, walkableSurfaces);
        agent.SetDestination(proxyTarget);
    }
}
