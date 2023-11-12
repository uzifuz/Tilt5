using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControllableEntity : Entity
{
    public NavMeshAgent agent;
    public float agentWalkSpeed = 1f, agentRunSpeed = 4f;
    [SerializeField]
    LayerMask walkableSurfaces;
    public bool CanMove;
    public Animator anim;

    protected override void InheritStart()
    {
        base.InheritStart();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
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
            agent.speed = agentRunSpeed;
            anim.SetFloat("animSpeed", 4);
        }
        else
        {
            agent.speed = agentWalkSpeed;
            anim.SetFloat("animSpeed", 1);
        }
        Vector3 proxyTarget = NavMeshInfo.RandomNavSphere(target, 0f, radiusMod, walkableSurfaces);
        agent.SetDestination(proxyTarget);
    }
}
