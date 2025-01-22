using UnityEngine;

public class ArcherBedevi : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] public float attackRange = 5f; // Ok atma menzili
    [SerializeField] public Transform target;
    [SerializeField] public Transform shootPoint;
    [SerializeField] public GameObject arrowPrefab;
    [SerializeField] public float shootCooldown = 2f;
    [SerializeField] public Transform[] patrolPoints;
    public float shootTimer;
    public Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private bool facingRight = true;
    private int currentPatrolIndex = 0;

    public IArcherBedeviState currentState;

    public readonly ArcherBedeviIdleState idleState = new ArcherBedeviIdleState();
    public readonly ArcherBedeviPatrolState patrolState = new ArcherBedeviPatrolState();
    public readonly ArcherBedeviAttackState attackState = new ArcherBedeviAttackState();

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        shootTimer = shootCooldown;

        TransitionToState(idleState);
    }

    private void Update()
    {
        shootTimer -= Time.deltaTime;
        currentState.UpdateState(this);
    }

    public void TransitionToState(IArcherBedeviState newState)
    {
        currentState?.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    public float DistanceToPlayer()
    {
        return Vector2.Distance(transform.position, target.position);
    }

    public void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        Transform targetPatrolPoint = patrolPoints[currentPatrolIndex];
        Vector2 direction = (targetPatrolPoint.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

        if (Vector2.Distance(transform.position, targetPatrolPoint.position) < 0.1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }

        if (direction.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (direction.x < 0 && facingRight)
        {
            Flip();
        }
    }

    public void Shoot()
    {
        animator.SetTrigger("Shoot");
    }

    public void CreateArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, shootPoint.rotation);
        Arrow arrowScript = arrow.GetComponent<Arrow>();

        if (arrowScript != null)
        {
            arrowScript.SetDirection(facingRight ? Vector2.right : Vector2.left);
        }
    }

    public void Flip()
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = !facingRight;
    }

    public void FaceTarget() // Metodun koruma düzeyi public yapýldý
    {
        if (target == null) return;

        if (transform.position.x < target.position.x && !facingRight)
        {
            Flip();
        }
        else if (transform.position.x > target.position.x && facingRight)
        {
            Flip();
        }
    }
}
