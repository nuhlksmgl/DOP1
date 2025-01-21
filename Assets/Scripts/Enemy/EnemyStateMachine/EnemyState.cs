public enum EnemyState
{
    Idle,
    Chase,
    Attack
}

public interface IEnemyState
{
    void EnterState(Enemy enemy);
    void UpdateState(Enemy enemy);
    void ExitState(Enemy enemy);
}
