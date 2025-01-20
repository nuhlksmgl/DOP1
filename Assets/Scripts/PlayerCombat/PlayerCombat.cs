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
        if (Input.GetMouseButtonDown(0)) // Sol mouse butonuna basýldýðýnda saldýrý
        {
            Attack();
        }
    }

    private void Attack()
    {
        // Saldýrý animasyonunu tetikleme
        animator.SetBool("isAttacking", true);

        // Saldýrý alanýný belirleme
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayers);

        // Düþmanlara zarar verme
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("enemy")) // Sadece 'enemy' tag'ine sahip objeleri hedef al
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
        }

        // Saldýrý animasyonunu durdurma
        StartCoroutine(ResetAttack());
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.5f); // Saldýrý animasyonunun süresine göre ayarlayýn
        animator.SetBool("isAttacking", false);
    }

    // Saldýrý alanýný görselleþtirmek için
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
