using UnityEngine;
public class AttackState : StaticState
{
    private readonly Player _player;
    private float _attackTimer = 1f;

    public AttackState(IStateSwitcher iStateSwitcher, MovementStateMachineData data, Player player) : base(
        iStateSwitcher, data, player)
    {
        _player = player;
    }

    public override void Enter()
    {
        _player.SetAttackMode(true);
        _player.View.StartAttacking();
        _attackTimer = 1f;
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