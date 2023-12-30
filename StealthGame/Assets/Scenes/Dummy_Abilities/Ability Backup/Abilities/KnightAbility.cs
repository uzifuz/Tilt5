using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KnightAbility : Ability
{
    //private Transform attackPoint;
    //private float attackRange = 1.0f;
    //private LayerMask enemyLayer;

    public void UseAbility(Transform attackPoint, float attackRange, LayerMask enemyLayer)
    {
        Cooldown = .66f;
        if(AbilityReady)
        {
            StartCoolDown();
            Thief.Instance.KnightAbility();
            /*Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);
            foreach (Collider enemy in hitEnemies)
            {
                ControllableEntity guard = enemy.GetComponent<ControllableEntity>();
                if(!guard.ModifyHealth(-100f))
                {
                    guard.Death(attackPoint.forward * Random.Range(5f, 10f));
                }
                Debug.LogWarning("Did a damage on " + enemy.name);
            }*/
        }
    }
}
