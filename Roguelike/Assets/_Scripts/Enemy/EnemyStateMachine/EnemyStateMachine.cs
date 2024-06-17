public class EnemyStateMachine: IEnemyStateSwitcher
{
    public IEnemyState CurrentEnemyState { get; private set; }

    public void Initialize(IEnemyState startingState)
    {
        CurrentEnemyState = startingState;
        CurrentEnemyState.EnterState();
    }

    public void SwitchState(IEnemyState newState)
    {
        CurrentEnemyState.ExitState();
        CurrentEnemyState = newState;
        CurrentEnemyState.EnterState();
    }
}
