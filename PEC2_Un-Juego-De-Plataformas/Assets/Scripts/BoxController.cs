using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public GameObject potion;

    private AudioSource audioSource;
    private Animator animator;
    private BoxCollider2D[] colliders;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        colliders = GetComponents<BoxCollider2D>();
    }

    public void PlayerInteract()
    {
        audioSource.Play();
        foreach(BoxCollider2D bx in colliders)
        {
            bx.enabled = false;
        }

        animator.SetTrigger("break");
        GameObject newObject =  Instantiate(potion, transform.position, Quaternion.identity, null);        
        Destroy(gameObject, 3);
    }
}
