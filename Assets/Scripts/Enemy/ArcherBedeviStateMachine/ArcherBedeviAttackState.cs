using UnityEngine;

public class ArcherBedeviAttackState : IArcherBedeviState
{
    public void EnterState(ArcherBedevi archerBedevi)
    {
        archerBedevi.animator.SetTrigger("Shoot");
    }

    public void UpdateState(ArcherBedevi archerBedevi)
    {
        archerBedevi.FaceTarget(); // Hedefe yönelme

        if (archerBedevi.DistanceToPlayer() > archerBedevi.attackRange)
        {
            archerBedevi.TransitionToState(archerBedevi.idleState);
        }
        else if (archerBedevi.shootTimer <= 0f)
        {
            archerBedevi.Shoot();
            archerBedevi.shootTimer = archerBedevi.shootCooldown;
        }
    }

    public void ExitState(ArcherBedevi archerBedevi)
    {
        archerBedevi.animator.ResetTrigger("Shoot");
    }
}
