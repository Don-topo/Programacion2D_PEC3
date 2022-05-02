using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionController : MonoBehaviour
{

    private AudioSource audioSource;
    private SpriteRenderer sprite;
    private BoxCollider2D[] colliders;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
        colliders = GetComponents<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioSource.Play();
            sprite.enabled = false;
            foreach(BoxCollider2D bc in colliders)
            {
                bc.enabled = false;
            }
            GameManager.Instance.PickUpPotion();
            Destroy(gameObject, audioSource.clip.length);
        }
    }

}
