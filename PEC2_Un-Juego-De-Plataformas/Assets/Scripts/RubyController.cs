using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public int rubyValue = 1;
    private SpriteRenderer sprite;
    private BoxCollider2D[] colliders;
    private AudioSource audioSource;
    private Animator animator;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        colliders = GetComponents<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioSource.Play();
            sprite.enabled = false;
            animator.enabled = false;
            foreach (BoxCollider2D bc in colliders)
            {
                bc.enabled = false;
            }
            //gameManager.PickUpRuby(rubyValue);
            GameManager.Instance.PickUpRuby(rubyValue);
            Destroy(gameObject, audioSource.clip.length);
        }
    }
}
