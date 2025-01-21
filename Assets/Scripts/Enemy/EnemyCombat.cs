using UnityEngine;
using System.Collections;

public class EnemyCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    public float attackRange = 1f; // Saldýrý mesafesi
    public float stopRange = 2f; // Düþmanýn duracaðý mesafe
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
        if (animator == null)
        {
            Debug.LogError("Animator bulunamadý.");
        }

        player = GameObject.FindWithTag(playerTag)?.transform;
        if (player == null)
        {
            Debug.LogError("Player bulunamadý.");
        }

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D bulunamadý.");
        }
    }

    private void Update()
    {
        if (player == null || attackPoint == null) return; // Referanslar null ise devam etme

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange && canAttack)
        {
            rb.velocity = Vector2.zero; // Bedevi'nin durmasýný saðla
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
        // Bedevi durur ve saldýrý animasyonunu tetikler
        Debug.Log("Bedevi saldýrýya geçti."); // Saldýrý baþladýðýnda log ekle
        animator.SetTrigger("BedeviMeleeCombat"); // Saldýrý için trigger kullanarak animasyonu baþlat

        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        foreach (Collider2D player in hitPlayers)
        {
            if (player.CompareTag(playerTag))
            {
                Debug.Log("Bedevi oyuncuya vurdu."); // Bedevi oyuncuya vurduðunda log ekle
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
        Debug.Log("Bedevi saldýrýsýný tamamladý."); // Saldýrý tamamlandýðýnda log ekle
        animator.ResetTrigger("BedeviMeleeCombat"); // Saldýrý animasyonunu sýfýrla
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
