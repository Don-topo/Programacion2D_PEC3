using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinController : EnemyController
{
    
    public SpriteRenderer healthSprite;
    public Sprite[] healtBars;
    public ParticleSystem rushParticles;
    public AudioClip rushClip;
    public AudioClip groundedClip;
    public Transform groundTransform;
    [SerializeField]
    LayerMask floorMask;

    private float jumpForce = 3f;
    private float rushForce = 15f;
    private bool grounded = true;

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
        if (!isAttacking)
        {
            // TODO select attack type randomly
            //RegularAttack();
            //Rush();
            CheckGrounded();
            Jump();
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
            var move = new Vector3(playerPosition.position.x, transform.position.y, transform.position.z);
            var dir = (move - transform.position).normalized * movement;
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
            GameManager.Instance.LevelCompleted(4);
        }
        base.GetHit(damage);
        healthSprite.sprite = healtBars[healtPoints];
    }    

    private void Rush()
    {
        StartCoroutine(Rushing());
    }

    private void RegularAttack()
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

    private void Jump()
    {
        isAttacking = true;
        if (enemyIsFacingRight)
        {
            //rigidbody2D.AddForce(Vector2.left * jumpForce, ForceMode2D.Impulse);
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.velocity = new Vector2(10, jumpForce);
        }
        else
        {
            //rigidbody2D.AddForce(Vector2.right * jumpForce, ForceMode2D.Impulse);
            rigidbody2D.velocity = new Vector2(-10, jumpForce);
        }
        isAttacking = false;
    }

    IEnumerator Rushing()
    {
        int direction;
        isAttacking = true;
        rigidbody2D.velocity = Vector2.zero;
        yield return new WaitForSeconds(2);
        if (enemyIsFacingRight)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
        rigidbody2D.velocity = Vector2.zero;
        rigidbody2D.AddForce(new Vector2(rushForce * direction, 0), ForceMode2D.Impulse);
        rushParticles.Play();
        audioSource.clip = rushClip;
        audioSource.Play();
        yield return new WaitForSeconds(0.4f);
        rigidbody2D.velocity = Vector2.zero;
        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }

    private void CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundTransform.position, Vector2.down, 0.25f, floorMask);

        if (hit == true)
        {
            if (!grounded)
            {
                audioSource.clip = groundedClip;
                audioSource.Play();
            }
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

}
