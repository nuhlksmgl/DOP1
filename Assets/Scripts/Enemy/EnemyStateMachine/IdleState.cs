using UnityEngine;

public class IdleState : IEnemyState
{
    public void EnterState(Enemy enemy)
    {
        enemy.animator.SetBool("BedeviWalking", false);
        enemy.animator.SetBool("BedeviIdle", true);
    }

    public void UpdateState(Enemy enemy)
    {
        if (enemy.DistanceToPlayer() <= enemy.chaseRange)
        {
            enemy.TransitionToState(enemy.chaseState);
        }
        else
        {
            enemy.Patrol();
        }
    }

    public void ExitState(Enemy enemy) { }
}
