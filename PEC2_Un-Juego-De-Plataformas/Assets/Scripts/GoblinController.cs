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

    private float rushForce = 15f;    


    public GoblinController()
    {
        healtPoints = 5;
        attackRange = 50f;
        damage = 1;
        playerRange = 10f;
        movement = 3.5f;
        attackSpeed = 1.5f;        
    }

    protected override void Attack()
    {
        if (!isAttacking)
        {
            RegularAttack();
        }          
    }

    protected override void Move()
    {
        float distance = Mathf.Abs(transform.position.x - playerPosition.transform.position.x);

        if (distance < playerRange && !isAttacking)
        {
            CheckFlip();
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
            Death();
            GameManager.Instance.LevelCompleted(4);
        }
        else
        {
            base.GetHit(damage);
            healthSprite.sprite = healtBars[healtPoints - 1];
        }        
    }  

    private void RegularAttack()
    {
        float distance = Mathf.Abs(playerPosition.position.x - attackZone.transform.position.x);
        if (distance <= attackRange)
        {
            int randomValue = Random.Range(0, 100);
            if(randomValue < 65)
            {
                StartCoroutine(MeleeAttack());
            }
            else
            {
                StartCoroutine(Rushing());
            }
        }
        else
        {
            attackTime = 0f;
        }      
    }

    IEnumerator MeleeAttack()
    {
        rigidbody2D.velocity = Vector2.zero;
        isAttacking = true;
        yield return new WaitForSeconds(attackTime);
        base.Attack();
        isAttacking = false;        
    }

    private void CheckFlip()
    {
        if (playerPosition.transform.position.x < transform.position.x && enemyIsFacingRight)
        {
            Flip();
        }
        else if (playerPosition.transform.position.x > transform.position.x && !enemyIsFacingRight)
        {
            Flip();
        }
    }

    IEnumerator Rushing()
    {
        int direction;
        isAttacking = true;
        rigidbody2D.velocity = Vector2.zero;
        yield return new WaitForSeconds(2);
        CheckFlip();
        if (playerPosition.transform.position.x > transform.position.x)
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

    private int CalculatePercentage()
    {
       return Random.Range(0, 100);
    }
}
