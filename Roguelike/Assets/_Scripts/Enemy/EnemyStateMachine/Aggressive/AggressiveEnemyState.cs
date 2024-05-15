public class AggressiveEnemyState
{
    protected readonly AggressiveEnemy aggressiveEnemy;
    protected AggressiveEnemyStateMachine aggressiveEnemyStateMachine;

    protected AggressiveEnemyState(AggressiveEnemy aggressiveEnemy, AggressiveEnemyStateMachine aggressiveEnemyStateMachine)
    {
        this.aggressiveEnemy = aggressiveEnemy;
        this.aggressiveEnemyStateMachine = aggressiveEnemyStateMachine;
    }

    public void EnterState() {}
    public void ExitState() {}
    public virtual void FrameUpdate() {}
    public void FixedUpdate() {}
}
