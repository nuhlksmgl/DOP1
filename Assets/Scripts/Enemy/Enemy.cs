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

    [HideInInspector] public bool canAttack = true;

    private Transform player;
    private IEnemyState currentState;
    public readonly IdleState idleState = new IdleState();
    public readonly ChaseState chaseState = new ChaseState();
    public readonly AttackState attackState = new AttackState();

    private Rigidbody2D rb;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        player = GameObject.FindWithTag(playerTag)?.transform;
        rb = GetComponent<Rigidbody2D>();

        if (animator == null) Debug.LogError("Animator bulunamadý.");
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

    public void ChasePlayer()
    {
        if (DistanceToPlayer() > attackRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero; // Saldýrý menziline girdiyse durdur
        }
    }
}
