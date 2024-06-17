public class AggressiveEnemyState: IEnemyState
{
    protected readonly AggressiveEnemy aggressiveEnemy;

    protected AggressiveEnemyState(AggressiveEnemy aggressiveEnemy)
    {
        this.aggressiveEnemy = aggressiveEnemy;
    }

    public virtual void EnterState() {}
    public virtual void ExitState() {}
    public virtual void FrameUpdate() {}
    public void FixedUpdate() {}
}
