using UnityEngine;

public class SpecialAttackState : StaticState
{
    private readonly MovementStateConfig _movementStateConfig;
    private readonly Player _player;
    private float _attackTimer = 0.85f;

    public SpecialAttackState(IStateSwitcher iStateSwitcher, MovementStateMachineData data, Player player) : base(
        iStateSwitcher, data, player)
    {
        _player = player;
    }

    public override void Enter()
    {
        _player.SetAttackMode(true);
        _player.View.StartSpecialAttacking();
        _attackTimer = 0.85f;
    }

    public override void Exit()
    {
        base.Exit();
        _player.SetAttackMode(false);
    }
    
    public override void Update()
    {
        _attackTimer -= Time.deltaTime;

        if (!(_attackTimer <= 0)) return;

        base.Update();
    }
}