using UnityEngine;

public class AttackState : IEnemyState
{
    public void EnterState(Enemy enemy)
    {
        // Saldýrý iþlemleri
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(enemy.attackPoint.position, enemy.attackRange);
        foreach (Collider2D player in hitPlayers)
        {
            if (player.CompareTag(enemy.playerTag))
            {
                Debug.Log("Bedevi oyuncuya vurdu!");
                player.GetComponent<PlayerHealth>()?.TakeDamage(enemy.attackDamage);
                Knockback(player.transform, enemy);
            }
        }

        enemy.animator.SetTrigger("BedeviMeleeCombat");
        enemy.canAttack = false;
        enemy.Invoke(nameof(enemy.ResetAttack), enemy.attackCooldown);
    }

    public void UpdateState(Enemy enemy)
    {
        if (enemy.DistanceToPlayer() > enemy.attackRange)
        {
            enemy.TransitionToState(enemy.chaseState);
        }
    }

    public void ExitState(Enemy enemy) { }

    private void Knockback(Transform player, Enemy enemy)
    {
        Vector2 knockbackDirection = (player.position - enemy.transform.position).normalized;
        player.GetComponent<Rigidbody2D>()?.AddForce(knockbackDirection * enemy.knockbackForce, ForceMode2D.Impulse);
    }
}
