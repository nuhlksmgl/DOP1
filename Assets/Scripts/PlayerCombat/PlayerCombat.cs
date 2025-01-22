using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    public float attackRange = 1f;
    public int attackDamage = 10;
    public string enemyTag = "Enemy";
    public Transform attackPoint;
    public float knockbackForce = 5f;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        if (animator == null) Debug.LogError("Animator bulunamad�.");
        if (attackPoint == null) Debug.LogError("AttackPoint referans� atanmad�.");
    }

    private void Update()
    {
        // Sol t�klama ile sald�r�y� ba�lat
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void Attack()
    {
        animator.SetTrigger("isAttacking");

        // Sald�r� alan�ndaki d��manlar� bul
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag(enemyTag))
            {
                Debug.Log("Player d��mana vurdu!");
                enemy.GetComponent<ArcherBedeviHealth>()?.TakeDamage(attackDamage); // ArcherBedevi'ye hasar verme
                enemy.GetComponent<EnemyHealth>()?.TakeDamage(attackDamage); // Di�er d��manlara hasar verme
                Knockback(enemy.transform);
            }
        }

        StartCoroutine(ResetAttack());
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.5f);
        animator.ResetTrigger("isAttacking"); // Animasyon tamamland���nda trigger'� s�f�rla
    }

    private void Knockback(Transform enemy)
    {
        Vector2 knockbackDirection = (enemy.position - transform.position).normalized;
        enemy.GetComponent<Rigidbody2D>()?.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
