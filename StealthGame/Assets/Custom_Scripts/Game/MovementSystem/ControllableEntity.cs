using JetBrains.Annotations;
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
    protected bool actionLock = false;
    public Animator anim;
    public RagdollMaster ragdoll;
    public float AttackRange = 2f, AttackSpeed = 2f, ProjectileSpeed = 14f;
    protected bool ReadyToAttack = true;
    public GameObject AttackProjectile;
    [SerializeField] protected Transform firePoint;

    protected override void InheritStart()
    {
        base.InheritStart();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        ragdoll = GetComponentInChildren<RagdollMaster>();
    }

    protected override void InheritUpdate()
    {
        base.InheritUpdate();
        if(agent.enabled == true)
        {
            if(Vector3.Distance(transform.position, agent.destination) < 0.15f)
            {
                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;
            }
        }
    }

    public void SetAgentDestination(Vector3 target, bool run = false, float radiusMod = 1f)
    {
        if(!actionLock)
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
            //Check if Stuff is actually on the NavMeshSurface
            if(NavMeshInfo.IsDestinationOnNavMesh(target))
            {
                //Debug.Log($"Destination IS on Navmesh");
                agent.SetDestination(target);
            }
            else
            {
                //Debug.Log($"Destination NOT on Navmesh");
                //Vector3 proxyTarget = NavMeshInfo.RandomNavSphere(target, 0f, radiusMod, walkableSurfaces);
                //agent.SetDestination(proxyTarget);
            }

        }
    }

    public override void Death(Vector3 deathDirection)
    {
        Debug.Log($"Death {name}");
        agent.enabled = false;
        anim.enabled = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = true;
        ragdoll.StartRagdolling(deathDirection.normalized, deathDirection.magnitude);
    }

    public void Attack()
    {
        var obj = Instantiate(AttackProjectile, firePoint.transform.position, transform.rotation);
        if(this is Thief)
        {
            obj.GetComponent<ProjectileBase>().SetupProjectile(false, false, this, ProjectileSpeed);
        }
        else
        {
            obj.GetComponent<ProjectileBase>().SetupProjectile(false, false, this, ProjectileSpeed, Thief.Instance.transform);
        }
    }

    protected IEnumerator AttackSpeedCo()
    {
        agent.SetDestination(transform.position);
        agent.isStopped = true;
        ReadyToAttack = false;
        yield return new WaitForSeconds(1f / AttackSpeed);
        ReadyToAttack = true;
        agent.isStopped = false;
    }
}
