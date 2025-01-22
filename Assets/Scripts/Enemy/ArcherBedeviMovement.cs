using UnityEngine;

public class ArcherBedeviMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float attackRange = 5f; // Ok atma menzili
    [SerializeField] private Transform target;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float shootCooldown = 2f;
    [SerializeField] private Transform[] patrolPoints;
    private float shootTimer;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool facingRight = true;
    private int currentPatrolIndex = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        shootTimer = shootCooldown;

        if (animator == null) Debug.LogError("Animator bile�eni atanmad�! L�tfen bir Animator bile�eni ekleyin.");
        if (spriteRenderer == null) Debug.LogError("SpriteRenderer bile�eni atanmad�! L�tfen bir SpriteRenderer bile�eni ekleyin.");
        if (shootPoint == null) Debug.LogError("ShootPoint referans� atanmad�.");
        if (arrowPrefab == null) Debug.LogError("ArrowPrefab atanmad�.");
        if (patrolPoints.Length == 0) Debug.LogError("Patrol noktalar� atanmad�.");
    }

    private void Update()
    {
        shootTimer -= Time.deltaTime;

        if (target != null && Vector2.Distance(transform.position, target.position) <= attackRange)
        {
            rb.velocity = Vector2.zero; // Hedef menzile girdi�inde dur
            animator.SetBool("isWalking", false);
            animator.SetBool("isIdle", true);

            if (shootTimer <= 0f)
            {
                Shoot();
                shootTimer = shootCooldown;
            }
        }
        else
        {
            Patrol(); // Patrol noktalar� aras�nda devriye gezme
        }
    }

    private void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        Transform targetPatrolPoint = patrolPoints[currentPatrolIndex];
        Vector2 direction = (targetPatrolPoint.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

        if (Vector2.Distance(transform.position, targetPatrolPoint.position) < 0.1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }

        animator.SetBool("isWalking", true);
        animator.SetBool("isIdle", false);

        if (direction.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (direction.x < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Shoot()
    {
        animator.SetTrigger("Shoot");
    }

    // Bu metod animasyon eventi taraf�ndan �a�r�lacak
    public void CreateArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, shootPoint.rotation);
        Arrow arrowScript = arrow.GetComponent<Arrow>();

        if (arrowScript != null)
        {
            arrowScript.SetDirection(facingRight ? Vector2.right : Vector2.left);
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = !facingRight;
    }
}
