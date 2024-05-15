public class AggressiveEnemyStateMachine 
{
    public AggressiveEnemyState CurrentAggressiveEnemyState { get; set; }

    public void Initialize(AggressiveEnemyState startingState)
    {
        CurrentAggressiveEnemyState = startingState;
        CurrentAggressiveEnemyState.EnterState();
    }

    public void SwitchState(AggressiveEnemyState newState)
    {
        CurrentAggressiveEnemyState.ExitState();
        CurrentAggressiveEnemyState = newState;
        CurrentAggressiveEnemyState.EnterState();
    }
}
