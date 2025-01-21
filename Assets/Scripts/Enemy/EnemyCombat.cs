using UnityEngine;
using System.Collections;

public class EnemyCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    public float attackRange = 1f;
    public int attackDamage = 10;
    public string playerTag = "Player"; // Oyuncunun tag'ý
    public float attackCooldown = 1.5f;
    public float moveSpeed = 2f; // Bedevi'nin yürüme hýzý
    public Transform attackPoint; // Saldýrý noktasýnýn referansý

    private Animator animator;
    private Transform player;
    private bool canAttack = true;
    private Rigidbody2D rb;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        player = GameObject.FindWithTag(playerTag).transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange && canAttack)
        {
            rb.velocity = Vector2.zero; // Bedevi'nin durmasýný saðla
            Attack();
        }
        else if (distanceToPlayer > attackRange && distanceToPlayer <= attackRange * 2) // Oyuncuya yaklaþýnca
        {
            ChasePlayer();
        }
        else
        {
            animator.SetBool("BedeviWalking", true);
            animator.SetBool("BedeviMeleeCombat", false);
        }
    }

    private void Attack()
    {
        // Bedevi durur ve saldýrý animasyonunu tetikler
        animator.SetBool("BedeviMeleeCombat", true);
        animator.SetBool("BedeviWalking", false);

        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        foreach (Collider2D player in hitPlayers)
        {
            if (player.CompareTag(playerTag))
            {
                player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
            }
        }

        StartCoroutine(ResetAttack());
    }

    private void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        animator.SetBool("BedeviWalking", true);
        animator.SetBool("BedeviMeleeCombat", false);
    }

    private IEnumerator ResetAttack()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
