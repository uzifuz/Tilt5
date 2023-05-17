using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControllableEntity : Entity
{
    public NavMeshAgent agent;
    [SerializeField]
    LayerMask walkableSurfaces;
    public bool CanMove;

    protected override void InheritStart()
    {
        base.InheritStart();
        agent = GetComponent<NavMeshAgent>();
    }

    protected override void InheritUpdate()
    {
        base.InheritUpdate();
        if(Vector3.Distance(transform.position, agent.destination) < 0.15f)
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }
    }

    public void SetAgentDestination(Vector3 target, bool run = false, float radiusMod = 1f)
    {
        if(run)
        {
            agent.speed = 6f;
            GetComponent<Animator>().SetFloat("animSpeed", 20);
        }
        else
        {
            agent.speed = 1.5f;
            GetComponent<Animator>().SetFloat("animSpeed", 5);
        }
        Vector3 proxyTarget = NavMeshInfo.RandomNavSphere(target, 0f, radiusMod, walkableSurfaces);
        //Debug.Log($"{name} was set to {proxyTarget}");
        agent.SetDestination(proxyTarget);
    }
}
