using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    protected int healtPoints = 4;
    protected int damage = 1;
    protected Animator animator;
    protected Collider2D[] colliders2D;
    protected AudioSource audioSource;
    protected float hitForce = 5f;
    protected new Rigidbody2D rigidbody2D;
    protected bool enemyIsFacingRight = true;
    protected float movement = 0;
    protected bool canMove = true;
    protected float attackSpeed = 2f;
    protected bool isAttacking = false;
    protected float attackTime = 0f;


    public Transform attackZone;
    public float attackRange = 5.0f;
    public float playerRange = 20f;
    public LayerMask attackLayer;
    public Transform playerPosition;
    public AudioClip hitClip;
    public AudioClip attackClip;

    // Start is called before the first frame update
    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        colliders2D = gameObject.GetComponents<Collider2D>();
        audioSource = gameObject.GetComponent<AudioSource>();
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(healtPoints > 0 && canMove)
        {
            Move();
            Attack();
        }        
    }

    public virtual void GetHit(int damage)
    {
        animator.SetTrigger("Hit");
        StartCoroutine(WaitHit());
        healtPoints -= damage;
        audioSource.clip = hitClip;
        audioSource.Play();
        if(healtPoints <= 0)
        {
            Death();
        }
        else
        {
            if (enemyIsFacingRight)
            {
                rigidbody2D.AddForce(Vector2.left * hitForce, ForceMode2D.Impulse);
            }
            else
            {
                rigidbody2D.AddForce(Vector2.right * hitForce, ForceMode2D.Impulse);
            }
        }
    }

    protected void Death()
    {
        animator.SetTrigger("Death");
        rigidbody2D.velocity = Vector2.zero;
        rigidbody2D.isKinematic = true;
        foreach(Collider2D cl in colliders2D)
        {
            cl.enabled = false;
        }        
    }

    protected virtual void Move() { }

    protected virtual void Attack()
    {
        animator.SetTrigger("Attack");
        audioSource.clip = attackClip;
        audioSource.Play();
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackZone.position, attackRange, attackLayer);
        foreach (Collider2D cl in hitObjects)
        {
            if (cl.gameObject.CompareTag("Player"))
            {
                cl.GetComponent<PlayerController>().GetHit(damage);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().GetHit(damage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackZone == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackZone.position, attackRange);
    }

    protected void Flip()
    {
        enemyIsFacingRight = !enemyIsFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    protected IEnumerator WaitHit()
    {
        canMove = false;
        yield return new WaitForSeconds(1.5f);
        canMove = true;
    }  

}
