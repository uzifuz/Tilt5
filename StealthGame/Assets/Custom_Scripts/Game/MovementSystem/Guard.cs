using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Guard : ControllableEntity
{
    public enum GuardStates { Detected, Idle, Investigate }
    public enum GuardIdleStates { Guard, Patrolling}
    public GuardStates CurGuardState = GuardStates.Idle;
    public GuardIdleStates CurIdleState;
    public Transform GuardingPosition;
    public float randomSpread = 1f;
    public float patrolTime = 4f;
    public float hearingSensitivity = 1f;
    public float suspicionLevel = 0f;
    private float patrolTimer = 4f;
    [SerializeField]
    Image detectionImage;

    protected override void InheritStart()
    {
        base.InheritStart();
        if(GuardingPosition == null)
        {
            CurIdleState = GuardIdleStates.Patrolling;
        }
        else
        {
            CurIdleState = GuardIdleStates.Guard;
        }
    }

    protected override void InheritUpdate()
    {
        base.InheritUpdate();
        patrolTimer -= Time.deltaTime;
        if(suspicionLevel < 100)
        {
            suspicionLevel -= Time.deltaTime * (suspicionLevel / 2);
        }
        detectionImage.fillAmount = suspicionLevel / 100f;
        if(patrolTimer <= 0 && Vector3.Distance(transform.position, agent.destination) <= 1f || patrolTimer < -patrolTime)
        {
            patrolTimer = Random.Range(patrolTime / 2f, patrolTime);
            GuardRandomizedMovement();
        }
    }

    public void GuardRandomizedMovement()
    {
        Vector3 newPosition = transform.position;
        switch (CurIdleState)
        {
            case GuardIdleStates.Guard:
                newPosition = GuardingPosition.position + new Vector3(Random.Range(-1f, 1f) * randomSpread, 0, Random.Range(-1, 1f) * randomSpread);
                break;
            case GuardIdleStates.Patrolling:
                newPosition = transform.position + new Vector3(Random.Range(-1f, 1f) * randomSpread, 0, Random.Range(-1, 1f) * randomSpread);
                break;
        }
        SetAgentDestination(newPosition, false, 5f);
    }
}
