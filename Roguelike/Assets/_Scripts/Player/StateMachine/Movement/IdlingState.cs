
public class IdlingState: MovementState
{
    public IdlingState(IStateSwitcher stateSwitcher, MovementStateMachineData data, Player player) : base(stateSwitcher, data, player)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        View.StartIdling();
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
            return;
        
        stateSwitcher.SwitchState<RunningState>();
    }
}