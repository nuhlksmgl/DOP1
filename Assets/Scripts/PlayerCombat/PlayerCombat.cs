using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private LayerMask enemyLayers;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Sol mouse butonuna bas�ld���nda sald�r�
        {
            Attack();
        }
    }

    private void Attack()
    {
        // Sald�r� animasyonunu tetikleme
        animator.SetBool("isAttacking", true);

        // Sald�r� alan�n� belirleme
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayers);

        // D��manlara zarar verme
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("enemy")) // Sadece 'enemy' tag'ine sahip objeleri hedef al
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
        }

        // Sald�r� animasyonunu durdurma
        StartCoroutine(ResetAttack());
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.5f); // Sald�r� animasyonunun s�resine g�re ayarlay�n
        animator.SetBool("isAttacking", false);
    }

    // Sald�r� alan�n� g�rselle�tirmek i�in
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
