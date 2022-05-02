using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinController : EnemyController
{
    
    public SpriteRenderer healthSprite;
    public Sprite[] healtBars;
    private float jumpForce = 3f;

    public GoblinController()
    {
        healtPoints = 5;
        attackRange = 50f;
        damage = 1;
        playerRange = 10f;
        movement = 3.5f;
        attackSpeed = 0.5f;        
    }

    protected override void Attack()
    {

        float distance = Mathf.Abs(playerPosition.position.x - attackZone.transform.position.x);
        if (distance <= attackRange)
        {
            rigidbody2D.velocity = Vector2.zero;
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

        if (distance < playerRange && !isAttacking)
        {
            if (playerPosition.transform.position.x < transform.position.x && enemyIsFacingRight)
            {
                Flip();
            }
            else if (playerPosition.transform.position.x > transform.position.x && !enemyIsFacingRight)
            {
                Flip();
            }
            var test = new Vector3(playerPosition.position.x, transform.position.y, transform.position.z);
            var dir = (test - transform.position).normalized * movement;
            rigidbody2D.velocity = dir;
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }

    public override void GetHit(int damage)
    {        
        if(healtPoints - damage <= 0)
        {
            GameManager.Instance.LevelCompleted();
        }
        base.GetHit(damage);
        healthSprite.sprite = healtBars[healtPoints];
    }    

    private void Jump()
    {
        if (enemyIsFacingRight)
        {
            rigidbody2D.AddForce(Vector2.left * jumpForce, ForceMode2D.Impulse);
        }
        else
        {
            rigidbody2D.AddForce(Vector2.right * jumpForce, ForceMode2D.Impulse);
        }
    }

}
