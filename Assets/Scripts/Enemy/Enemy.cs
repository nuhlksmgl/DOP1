using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float attackRange = 1.5f;
    public float chaseRange = 3f;
    public int attackDamage = 10;
    public float attackCooldown = 1.5f;
    public float moveSpeed = 2f;
    public float knockbackForce = 5f;
    public Transform attackPoint;
    public string playerTag = "Player";
    public Animator animator;
    public Transform[] patrolPoints; // Patrol noktalarý eklendi
    private int currentPatrolIndex = 0;
    private bool facingRight = true; // Bu deðiþken eklendi
    private SpriteRenderer spriteRenderer; // Bu deðiþken eklendi

    [HideInInspector] public bool canAttack = true;
    public Transform player; // Bu deðiþkeni public yaptýk
    public IEnemyState currentState; // Bu deðiþkeni public yaptýk

    public readonly IdleState idleState = new IdleState();
    public readonly ChaseState chaseState = new ChaseState();
    public readonly AttackState attackState = new AttackState();

    private Rigidbody2D rb;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>(); // SpriteRenderer atanmasý eklendi
        player = GameObject.FindWithTag(playerTag)?.transform;
        rb = GetComponent<Rigidbody2D>();

        if (animator == null) Debug.LogError("Animator bulunamadý.");
        if (spriteRenderer == null) Debug.LogError("SpriteRenderer bulunamadý.");
        if (player == null) Debug.LogError("Player bulunamadý.");
        if (attackPoint == null) Debug.LogError("AttackPoint referansý atanmadý.");
        if (rb == null) Debug.LogError("Rigidbody2D bulunamadý.");

        TransitionToState(idleState);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void TransitionToState(IEnemyState newState)
    {
        currentState?.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    public float DistanceToPlayer()
    {
        return Vector2.Distance(transform.position, player.position);
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

    public void ChasePlayer()
    {
        if (DistanceToPlayer() > attackRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
            animator.SetBool("BedeviWalking", true); // Walking animasyonunu tetikleme
            animator.SetBool("BedeviIdle", false); // Idle animasyonunu durdurma
        }
        else
        {
            rb.velocity = Vector2.zero; // Saldýrý menziline girdiyse durdur
            animator.SetBool("BedeviWalking", false); // Walking animasyonunu durdurma
        }
    }

    public void ResetAttack()
    {
        canAttack = true;
        animator.ResetTrigger("BedeviMeleeCombat");
    }

    private void Flip()
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = !facingRight;
    }
}
