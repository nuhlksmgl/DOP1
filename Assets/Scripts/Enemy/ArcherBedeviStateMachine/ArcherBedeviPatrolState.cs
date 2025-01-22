using UnityEngine;

public class ArcherBedeviPatrolState : IArcherBedeviState
{
    public void EnterState(ArcherBedevi archerBedevi)
    {
        archerBedevi.animator.SetBool("isWalking", true);
        archerBedevi.animator.SetBool("isIdle", false);
    }

    public void UpdateState(ArcherBedevi archerBedevi)
    {
        archerBedevi.Patrol();
        if (archerBedevi.DistanceToPlayer() <= archerBedevi.attackRange)
        {
            archerBedevi.TransitionToState(archerBedevi.attackState);
        }
    }

    public void ExitState(ArcherBedevi archerBedevi)
    {
        archerBedevi.animator.SetBool("isWalking", false);
    }
}
