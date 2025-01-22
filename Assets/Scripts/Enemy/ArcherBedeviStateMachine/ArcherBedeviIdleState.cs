using UnityEngine;

public class ArcherBedeviIdleState : IArcherBedeviState
{
    public void EnterState(ArcherBedevi archerBedevi)
    {
        archerBedevi.animator.SetBool("isIdle", true);
        archerBedevi.animator.SetBool("isWalking", false);
    }

    public void UpdateState(ArcherBedevi archerBedevi)
    {
        if (archerBedevi.DistanceToPlayer() <= archerBedevi.attackRange)
        {
            archerBedevi.TransitionToState(archerBedevi.attackState);
        }
        else
        {
            archerBedevi.TransitionToState(archerBedevi.patrolState);
        }
    }

    public void ExitState(ArcherBedevi archerBedevi)
    {
        archerBedevi.animator.SetBool("isIdle", false);
    }
}
