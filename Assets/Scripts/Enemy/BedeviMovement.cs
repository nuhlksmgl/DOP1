using UnityEngine;

public class BedeviMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float knockbackForce = 5f;
    [SerializeField] private float knockbackDuration = 0.2f;
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 1.5f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Enemy enemy;

    private bool isKnockedBack = false;
    private float knockbackTimer = 0f;
    private bool facingRight = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        enemy = GetComponent<Enemy>();

        if (animator == null)
        {
            Debug.LogError("Animator bile�eni atanmad�! L�tfen bir Animator bile�eni ekleyin.");
        }

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer bile�eni atanmad�! L�tfen bir SpriteRenderer bile�eni ekleyin.");
        }

        if (enemy == null)
        {
            Debug.LogError("Enemy bile�eni atanmad�! L�tfen bir Enemy bile�eni ekleyin.");
        }
    }

    private void Update()
    {
        if (isKnockedBack)
        {
            knockbackTimer -= Time.deltaTime;
            if (knockbackTimer <= 0f)
            {
                isKnockedBack = false;
                rb.velocity = Vector2.zero; // Knockback s�resi bitince h�z s�f�rlan�r
                enemy.TransitionToState(enemy.idleState); // Knockback bitti�inde duruma d�n
            }
        }
        else if (enemy.currentState == enemy.chaseState)
        {
            MoveTowardsTarget();
        }
    }

    private void MoveTowardsTarget()
    {
        if (enemy.player != null)
        {
            Vector2 direction = (enemy.player.position - transform.position).normalized;
            rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

            animator.SetBool("BedeviWalking", true);
            animator.SetBool("BedeviIdle", false);
            if (direction.x > 0 && !facingRight)
            {
                Flip();
            }
            else if (direction.x < 0 && facingRight)
            {
                Flip();
            }
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = !facingRight;
    }

    public void ApplyKnockback(Vector2 direction)
    {
        if (!isKnockedBack)
        {
            isKnockedBack = true;
            knockbackTimer = knockbackDuration;

            rb.velocity = direction * knockbackForce;

            enemy.TransitionToState(enemy.idleState); // Knockback state'ine ge�
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
            ApplyKnockback(knockbackDirection);

            collision.gameObject.GetComponent<PlayerHealth>()?.TakeDamage(attackDamage);
        }
    }
}
