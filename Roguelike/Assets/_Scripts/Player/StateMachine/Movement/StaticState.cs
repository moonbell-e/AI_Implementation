using UnityEngine;

public class StaticState : IState
{
    private readonly IStateSwitcher _iStateSwitcher;
    private readonly MovementStateMachineData _data;
    private readonly Player _player;

    public StaticState(IStateSwitcher iStateSwitcher, MovementStateMachineData data, Player player)
    {
        _iStateSwitcher = iStateSwitcher;
        _data = data;
        _player = player;
    }

    public virtual void Enter()
    {
        //noon
    }

    public virtual void Exit()
    {
        _player.View.StopPlayback();
    }

    public virtual void HandleInput()
    {
        _data.InputDirection = _player.InputReader.InputDirection;
    }

    public virtual void Update()
    {
        if (_data.InputDirection == Vector2.zero)
        {
            _iStateSwitcher.SwitchState<IdlingState>();
        }
        else
        {
            _iStateSwitcher.SwitchState<MovementState>();
        }
    }
}
