using System.Collections.Generic;
using System.Linq;

public class PlayerStateMachine: IStateSwitcher
{
    private readonly List<IState> _states;
    private IState _currentState;

    public PlayerStateMachine(Player player)
    {
        var data = new MovementStateMachineData();

        _states = new List<IState>()
        {
            new IdlingState(this, data, player),
            new RunningState(this, data, player),
            new DashingState(this, data, player),
            new AttackState(this, data, player),
            new SpecialAttackState(this, data, player)
        };

        _currentState = _states[0];
        _currentState.Enter();
    }

    public void SwitchState<T>() where T: IState
    {
        var state = _states.FirstOrDefault(state => state is T);

        _currentState.Exit();
        _currentState = state;
        _currentState?.Enter();
    }

    public void HandleInput() => _currentState.HandleInput();

    public void Update() => _currentState.Update();
}

