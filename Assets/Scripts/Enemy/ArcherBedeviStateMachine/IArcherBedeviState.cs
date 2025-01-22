using UnityEngine;

public interface IArcherBedeviState
{
    void EnterState(ArcherBedevi archerBedevi);
    void UpdateState(ArcherBedevi archerBedevi);
    void ExitState(ArcherBedevi archerBedevi);
}
