using UnityEngine;

public class BedeviMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform[] patrolPoints; // Devriye noktalarý
    public float moveSpeed = 2f; // Bedevi'nin yürüme hýzý

    private int currentPointIndex = 0;
    private Animator animator;
    private Transform player;
    private EnemyCombat enemyCombat; // EnemyCombat bileþenine referans
    private bool facingRight = true;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        enemyCombat = GetComponent<EnemyCombat>(); // EnemyCombat bileþenini al
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > enemyCombat.attackRange * 2) // Oyuncu uzaktaysa devriye at
        {
            Patrol();
        }
        else if (distanceToPlayer <= enemyCombat.attackRange * 2) // Oyuncu yakýnsa onu kovalama
        {
            ChasePlayer();
        }

        Flip();
    }

    private void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, patrolPoints[currentPointIndex].position) < 0.1f)
        {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        }

        animator.SetBool("BedeviWalking", true);
        animator.SetBool("BedeviMeleeCombat", false);
    }

    private void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        animator.SetBool("BedeviWalking", true);
        animator.SetBool("BedeviMeleeCombat", false);
    }

    private void Flip()
    {
        if (player.position.x > transform.position.x && !facingRight || player.position.x < transform.position.x && facingRight)
        {
            facingRight = !facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
