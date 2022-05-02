using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : EnemyController
{

    public SkeletonController()
    {
        healtPoints = 1;
        attackRange = 5f;
        damage = 1;
        playerRange = 10f;
        movement = 0f;
        attackSpeed = 2f;
    }

    protected override void Attack()
    {
        float distance = Mathf.Abs(playerPosition.position.x - attackZone.transform.position.x);
        if(distance <= attackRange)
        {
            if (attackTime == 0f)
            {
                isAttacking = true;
                attackTime = Time.time + 1f / attackRange;
            }
            if (isAttacking)
            {
                if (Time.time >= attackTime)
                {
                    base.Attack();
                    isAttacking = false;
                }
            }
        }
        else
        {
            isAttacking = false;
            attackTime = 0f;
        }        
    }

    protected override void Move()
    {
        float distance = Mathf.Abs(transform.position.x - playerPosition.transform.position.x);
        if (distance < playerRange)
        {
            if(playerPosition.transform.position.x < transform.position.x && enemyIsFacingRight)
            {
                Flip();
            }
            else if(playerPosition.transform.position.x > transform.position.x && !enemyIsFacingRight)
            {
                Flip();
            }
        }
    }
}
