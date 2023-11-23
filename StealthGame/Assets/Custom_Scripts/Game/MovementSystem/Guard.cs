using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Guard : ControllableEntity
{
    public float GuardDifficulty = 1f;
    public enum GuardStates { Detected, Idle, Investigate }
    public enum GuardIdleStates { Guard, Patrolling }
    public enum GuardType { Melee, Ranged }
    public GuardStates CurGuardState = GuardStates.Idle;
    public GuardIdleStates CurIdleState;
    public GuardType BehaviorType = GuardType.Ranged;
    public Transform GuardingPosition;
    public bool PlayerHasBeenDetected = false;
    public float randomSpread = 1f;
    public float patrolTime = 4f;
    public float hearingSensitivity = 1f;
    public float suspicionLevel = 0f;
    public float visionTime = 100f;
    public float maxVisionRange = 15f;
    float visionTimer = 0f;
    private float patrolTimer = 4f;
    public float FOVAngleInDegrees = 30f;
    [SerializeField] LayerMask visionBlockingLayers;
    [SerializeField] Image detectionImage;
    [SerializeField] LineRenderer sightLine, suspicionLine;
    #region InheritedMethods

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
        if (Input.GetKey(KeyCode.P))
        {
            ModifyHealth(-1000f);
            Death(Vector3.up + Vector3.back * Random.Range(1f, 2f));
        }
        base.InheritUpdate();
        if(CurHealth > 0f)
        {
            detectionImage.fillAmount = suspicionLevel / 100f;
            if(patrolTimer <= 0 && Vector3.Distance(transform.position, agent.destination) <= 1f || patrolTimer < -patrolTime)
            {
                patrolTimer = Random.Range(patrolTime / 2f, patrolTime);
                GuardRandomizedMovement();
            }

            if(CanSeePlayer())
            {
                VisionOnPlayerBehavior();
                if(PlayerHasBeenDetected)//TODO: Attack the player
                {
                    if(Vector3.Distance(transform.position, Thief.Instance.transform.position) <= AttackRange)
                    {
                        //Attack
                        if(ReadyToAttack)
                        {
                            StartCoroutine(AttackSpeedCo());
                            anim.Play("Attack", 1);
                        }
                    }
                    else
                    {
                        //Move
                    }
                }
            }
            else
            {
                NoVisionBehavior();
            }
            visionTimer = Mathf.Clamp(visionTimer, 0f, visionTime);
        }
    }
    #endregion

    float AngleByDirection(Vector3 direction)
    {
        return Mathf.Acos(Vector3.Dot(transform.forward, direction)) * Mathf.Rad2Deg;
    }

    #region VisionBasedBehaviors
    void VisionOnPlayerBehavior()
    {
        sightLine.enabled = true;
        sightLine.SetPosition(0, sightLine.transform.position);
        sightLine.SetPosition(1, Thief.Instance.transform.position);
        suspicionLine.enabled = true;
        suspicionLine.SetPosition(0, suspicionLine.transform.position);
        suspicionLine.SetPosition(1, suspicionLine.transform.position + (Thief.Instance.transform.position - suspicionLine.transform.position) * visionTimer / visionTime);
        visionTimer += GuardDifficulty * (visionTime / 2f) * Time.deltaTime / Vector3.Distance(transform.position, Thief.Instance.transform.position);
        if(visionTimer >= visionTime - 2f)
        {
            suspicionLevel += Time.deltaTime * GuardDifficulty;
            if (suspicionLevel < 50)
                suspicionLevel = 50f;
        }
        if(suspicionLevel >= 50f)
        {
            PlayerHasBeenDetected = true;
        }
        patrolTimer = patrolTime;
    }

    void NoVisionBehavior()
    {
        sightLine.enabled = false;
        suspicionLine.enabled = false;
        if (suspicionLevel < 50f)
        {
            PlayerHasBeenDetected = false;
        }
        patrolTimer -= Time.deltaTime;
        visionTimer -= Time.deltaTime * 3f;
        suspicionLevel -= Time.deltaTime * (suspicionLevel / 2);
    }

    bool CanSeePlayer()
    {
        if(Thief.Instance != null)
        {
            RaycastHit hit;
            Vector3 direction = (Thief.Instance.transform.position - transform.position).normalized;
            if(AngleByDirection(direction) > FOVAngleInDegrees)
            {
                return false;
            }
            if (Physics.Raycast(transform.position, direction, out hit, maxVisionRange, visionBlockingLayers))
            {
                if(hit.collider.gameObject == Thief.Instance.gameObject && !Thief.Instance.IsHidden)
                {
                    Debug.DrawRay(transform.position, direction * hit.distance, Color.green);
                    if(PlayerHasBeenDetected)
                    {
                        agent.SetDestination(Thief.Instance.transform.position);
                    }
                    return true;
                }
                Debug.DrawRay(transform.position, direction * hit.distance, Color.yellow);
                return false;
            }
            else
            {
                Debug.DrawRay(transform.position, direction * hit.distance, Color.red);
                return false;
            }
        }
        return false;
    }
    #endregion


    #region Movement
    public void GuardRandomizedMovement()//Patrolling time is scuffed, possibly beyond repair! Scrap and redo!!!
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
    #endregion

    public override void Death(Vector3 deathDirection)
    {
        base.Death(deathDirection);
        suspicionLine.enabled = false;
        sightLine.enabled = false;
        StartCoroutine(DespawnCo());
    }

    IEnumerator DespawnCo()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
