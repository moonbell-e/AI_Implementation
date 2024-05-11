using UnityEngine;

public class DashingState : StaticState
{
    private readonly IStateSwitcher _iStateSwitcher;
    private readonly MovementStateMachineData _data;
    private readonly MovementStateConfig _movementStateConfig;
    private readonly Player _player;
    private float _dashTimer;


    public DashingState(IStateSwitcher stateSwitcher, MovementStateMachineData data, Player player) : base(
        stateSwitcher, data, player)
    {
        _data = data;
        _player = player;
        _movementStateConfig = player.Config.MovementConfig;
    }


    public override void Enter()
    {
        _data.DashForce = _movementStateConfig.DashForce;
        _data.DashDuration = _movementStateConfig.DashDuration;
        _dashTimer = _data.DashDuration;
        
        _player.View.StartDashing();
        _player.SetDashMode(true);
    }

    public override void Exit()
    {
        base.Exit();
        _player.Rb.velocity = Vector3.zero;
        _player.SetDashMode(false);
    }
    
    public override void Update()
    {
        var dashDirection = Quaternion.Euler(0, _data.LastRotationAngle, 0) * Vector3.forward;
        var velocity = dashDirection * _data.DashForce;
        _player.Rb.velocity = velocity;

        _dashTimer -= Time.deltaTime;
        
        if (!(_dashTimer <= 0)) return;

        base.Update();
    }
}