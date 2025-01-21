using UnityEngine;

public class ChaseState : IEnemyState
{
    public void EnterState(Enemy enemy)
    {
        enemy.animator.SetBool("BedeviWalking", true);
        enemy.animator.SetBool("BedeviIdle", false);
        enemy.animator.ResetTrigger("BedeviMeleeCombat");
    }

    public void UpdateState(Enemy enemy)
    {
        if (enemy.DistanceToPlayer() <= enemy.attackRange)
        {
            enemy.TransitionToState(enemy.attackState);
        }
        else if (enemy.DistanceToPlayer() > enemy.chaseRange)
        {
            enemy.TransitionToState(enemy.idleState);
        }
        else
        {
            enemy.ChasePlayer();
        }
    }

    public void ExitState(Enemy enemy)
    {
        enemy.animator.SetBool("BedeviWalking", false);
    }
}
