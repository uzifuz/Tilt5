using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : ControllableEntity
{
    public float randomSpread = 1f;
    public float patrolTime = 4f;
    public float hearingSensitivity = 1f;
    private float patrolTimer = 4f;

    protected override void InheritUpdate()
    {
        base.InheritUpdate();
        patrolTimer -= Time.deltaTime;
        if(patrolTimer <= 0)
        {
            patrolTimer = Random.Range(patrolTime / 2f, patrolTime);
            GuardRandomizedMovement();
        }
    }

    public void GuardRandomizedMovement()
    {
        Vector3 newPosition = transform.position + new Vector3(Random.Range(-1f, 1f) * randomSpread, 0, Random.Range(-1, 1f) * randomSpread);
        //SetAgentDestination(newPosition, false, 5f);
    }
}
