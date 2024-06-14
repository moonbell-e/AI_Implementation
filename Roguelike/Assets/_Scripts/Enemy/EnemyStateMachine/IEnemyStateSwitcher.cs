

public interface IEnemyStateSwitcher
{
    void Initialize(IEnemyState startingState);
    void SwitchState(IEnemyState newState);
}
