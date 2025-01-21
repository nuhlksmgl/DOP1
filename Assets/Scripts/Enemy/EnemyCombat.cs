using UnityEngine;
using System.Collections;

public class EnemyCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    public float attackRange = 1f; // Sald�r� mesafesi
    public float stopRange = 2f; // D��man�n duraca�� mesafe
    public int attackDamage = 10;
    public string playerTag = "Player"; // Oyuncunun tag'�
    public float attackCooldown = 1.5f;
    public float moveSpeed = 2f; // Bedevi'nin y�r�me h�z�
    public Transform attackPoint; // Sald�r� noktas�n�n referans�

    private Animator animator;
    private Transform player;
    private bool canAttack = true;
    private Rigidbody2D rb;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator bulunamad�.");
        }

        player = GameObject.FindWithTag(playerTag)?.transform;
        if (player == null)
        {
            Debug.LogError("Player bulunamad�.");
        }

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D bulunamad�.");
        }
    }

    private void Update()
    {
        if (player == null || attackPoint == null) return; // Referanslar null ise devam etme

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange && canAttack)
        {
            rb.velocity = Vector2.zero; // Bedevi'nin durmas�n� sa�la
            Attack();
        }
        else if (distanceToPlayer > attackRange && distanceToPlayer <= stopRange)
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
        // Bedevi durur ve sald�r� animasyonunu tetikler
        Debug.Log("Bedevi sald�r�ya ge�ti."); // Sald�r� ba�lad���nda log ekle
        animator.SetTrigger("BedeviMeleeCombat"); // Sald�r� i�in trigger kullanarak animasyonu ba�lat

        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        foreach (Collider2D player in hitPlayers)
        {
            if (player.CompareTag(playerTag))
            {
                Debug.Log("Bedevi oyuncuya vurdu."); // Bedevi oyuncuya vurdu�unda log ekle
                player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
            }
        }

        StartCoroutine(ResetAttack());
    }

    private void ChasePlayer()
    {
        if (Vector2.Distance(transform.position, player.position) > attackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
        animator.SetBool("BedeviWalking", true);
    }

    private IEnumerator ResetAttack()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        Debug.Log("Bedevi sald�r�s�n� tamamlad�."); // Sald�r� tamamland���nda log ekle
        animator.ResetTrigger("BedeviMeleeCombat"); // Sald�r� animasyonunu s�f�rla
        animator.SetBool("BedeviWalking", true);
        canAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, stopRange);
    }
}
