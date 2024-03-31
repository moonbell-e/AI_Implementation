public class RunningState: MovementState
{
    private readonly MovementStateConfig _movementStateConfig;

    public RunningState(IStateSwitcher stateSwitcher, MovementStateMachineData data, Player player) : base(
        stateSwitcher, data, player)
        => _movementStateConfig = player.Config.MovementConfig;
    
    public override void Enter()
    {
        base.Enter();
        data.Speed = _movementStateConfig.RunningSpeed;
        
        View.SetRunSpeed(data.Speed);
        View.StartRunning();
    }
    
    public override void Exit()
    {
        base.Exit();
        View.StopPlayback();
    }
    
    public override void Update()
    {
        base.Update();

        if (IsMovementInputZero())
            stateSwitcher.SwitchState<IdlingState>();
    }
}