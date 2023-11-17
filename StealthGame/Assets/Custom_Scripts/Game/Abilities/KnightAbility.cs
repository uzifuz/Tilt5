using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KnightAbility : Ability
{
    private Transform attackPoint;
    private float attackRange = 1.0f;
    private LayerMask enemyLayer;

    public void UseAbility(Animator animator, Transform attackPoint, float attackRange, LayerMask enemyLayer)
    {
        Cooldown = 5.0f;
        Debug.Log("Knight Ability");
        animator.SetTrigger("Knight_Slash");
        //stop player movement

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

        foreach(Collider enemy in hitEnemies)
        {
            Debug.Log("Hit " + enemy.name);
        }

        //Damage Enemy (+ enemy hit/death animation)

        //Cooldown
    }
}
