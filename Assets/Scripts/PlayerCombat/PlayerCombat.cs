using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    public float attackRange = 1f;
    public int attackDamage = 10;
    public string enemyTag = "Enemy"; // Düþmanlarýn tag'ý
    public float knockbackForce = 5f;
    public Transform attackPoint; // Saldýrý noktasýnýn referansý

    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void Attack()
    {
        animator.SetBool("BedeviMeleeCombat", true);
        animator.SetBool("BedeviWalking", false);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag(enemyTag))
            {
                enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
                Knockback(enemy.transform);
            }
        }

        StartCoroutine(ResetAttack());
    }

    private void Knockback(Transform enemy)
    {
        Vector2 knockbackDirection = enemy.position - transform.position;
        knockbackDirection.Normalize();
        enemy.GetComponent<Rigidbody2D>().AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("BedeviMeleeCombat", false);
        animator.SetBool("BedeviWalking", true);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
