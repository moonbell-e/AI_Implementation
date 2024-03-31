using UnityEngine;

public class PlayerAnimationDataHash
{
    private const string Idle = "Idle";
    private const string Run = "Run";
    private const string Dash = "Dash";
    private const string Attack = "Attack";
    private const string SpecialAttack = "SpecialAttack";
    private const string Speed = "MoveSpeed";
    
    public int IdleHash { get; } = Animator.StringToHash(Idle);
    public int RunHash { get; } = Animator.StringToHash(Run);
    public int DashHash { get; } = Animator.StringToHash(Dash);
    
    public int AttackHash { get; } = Animator.StringToHash(Attack);
    public int SpecialAttackHash { get; } = Animator.StringToHash(SpecialAttack);
    public int MoveSpeedHash { get; } = Animator.StringToHash(Speed);
}