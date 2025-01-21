using UnityEngine;

public class IdleState : IEnemyState
{
    public void EnterState(Enemy enemy)
    {
        enemy.animator.SetBool("BedeviIdle", true);
        enemy.animator.SetBool("BedeviWalking", false);
        enemy.animator.ResetTrigger("BedeviMeleeCombat");
    }

    public void UpdateState(Enemy enemy)
    {
        if (enemy.DistanceToPlayer() <= enemy.chaseRange)
        {
            enemy.TransitionToState(enemy.chaseState);
        }
    }

    public void ExitState(Enemy enemy)
    {
        enemy.animator.SetBool("BedeviIdle", false);
    }
}
