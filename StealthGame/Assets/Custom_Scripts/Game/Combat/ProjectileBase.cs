using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public Transform Target;
    public Vector3 NonHomingTarget;
    public float DamageDealt = 20;
    public bool Instant = false;
    public bool Homing = false;
    public float MoveSpeed = 12f;
    public GameObject CollisionInst;
    Entity origin;
    Rigidbody body;
    bool ready = false;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    public void SetupProjectile(bool writeInstant, bool writeHoming, Entity originEntity, float newSpeed, Transform writeTarget = null)
    {
        MoveSpeed = newSpeed;
        Instant = writeInstant;
        Homing = writeHoming;
        if (Homing)
            Target = writeTarget;
        else if (Instant)
            NonHomingTarget = writeTarget.position;
        if(writeTarget != null)
        {
            transform.LookAt(writeTarget);
        }
        origin = originEntity;
        ready = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(ready)
        {
            if(Instant)
            {
                transform.position = Target.position;
            }
            else
            {
                body.velocity = transform.forward * MoveSpeed;
                if(Homing)
                {
                    transform.LookAt(Target.position);
                }
                else
                {
                    //transform.LookAt(NonHomingTarget);
                }
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Collided with {other.name}");
        Entity newEntity = other.GetComponent<Entity>();
        if(newEntity != null && newEntity != origin)
        {
            if(!newEntity.ModifyHealth(-DamageDealt))
            {
                newEntity.Death(transform.forward * MoveSpeed);
            }
        }
        if(CollisionInst != null)
            Instantiate(CollisionInst, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
