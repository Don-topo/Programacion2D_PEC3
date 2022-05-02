using UnityEngine;

public class CoinController : MonoBehaviour
{

    public int coinValue = 1;
    private SpriteRenderer sprite;
    private BoxCollider2D[] colliders;
    private AudioSource audioSource;
    private Animator animator;

    private void Awake()
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
            foreach(BoxCollider2D bc in colliders)
            {
                bc.enabled = false;
            }
            GameManager.Instance.PickUpCoin(coinValue);
            Destroy(gameObject, audioSource.clip.length);           
        }
    }
}
