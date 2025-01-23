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
    public Transform[] patrolPoints; // Patrol noktalar�
    private int currentPatrolIndex = 0;
    private bool patrolForward = true; // Ping-pong hareketi i�in eklendi
    private bool facingRight = true;  // Y�z�n y�n�n� kontrol etmek i�in
    private SpriteRenderer spriteRenderer;

    [HideInInspector] public bool canAttack = true;
    public Transform player;
    public IEnemyState currentState;

    public readonly IdleState idleState = new IdleState();
    public readonly ChaseState chaseState = new ChaseState();
    public readonly AttackState attackState = new AttackState();

    private Rigidbody2D rb;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.FindWithTag(playerTag)?.transform;
        rb = GetComponent<Rigidbody2D>();

        if (animator == null) Debug.LogError("Animator bulunamad�.");
        if (spriteRenderer == null) Debug.LogError("SpriteRenderer bulunamad�.");
        if (player == null) Debug.LogError("Player bulunamad�.");
        if (attackPoint == null) Debug.LogError("AttackPoint referans� atanmad�.");
        if (rb == null) Debug.LogError("Rigidbody2D bulunamad�.");

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
    if (patrolPoints.Length == 0) return; // E�er patrol noktalar� yoksa hi�bir �ey yapma

    // �u anki hedef patrol noktas�n� belirle
    Transform targetPatrolPoint = patrolPoints[currentPatrolIndex];

    // Hedefe do�ru hareket et
    Vector2 direction = (targetPatrolPoint.position - transform.position).normalized;
    rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

    // E�er hedefe yeterince yakla��ld�ysa (�rne�in 0.1 birim mesafeden az)
    if (Vector2.Distance(transform.position, targetPatrolPoint.position) < 0.1f)
    {
        // E�er son noktadaysak geri d�n
        if (currentPatrolIndex == patrolPoints.Length - 1)
        {
            patrolForward = false; // Geri gitmeye ba�la
        }
        else if (currentPatrolIndex == 0)
        {
            patrolForward = true; // �leri gitmeye ba�la
        }

        // Index'i ileri ya da geri g�ncelle
        currentPatrolIndex += patrolForward ? 1 : -1;
    }

    // Animasyonlar� kontrol et
    animator.SetBool("BedeviWalking", true);
    animator.SetBool("BedeviIdle", false);

    // Sprite'� do�ru y�ne �evir
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
            animator.SetBool("BedeviWalking", true);
            animator.SetBool("BedeviIdle", false);
        }
        else
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("BedeviWalking", false);
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
