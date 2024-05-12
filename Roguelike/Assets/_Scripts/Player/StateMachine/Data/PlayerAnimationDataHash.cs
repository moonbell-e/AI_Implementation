using UnityEngine;

public class PlayerAnimationDataHash
{
    private const string Idle = "Idle";
    private const string Run = "Run";
    private const string Dash = "Dash";
    private const string Attack1 = "Attack1";
    private const string Attack2 = "Attack2";
    private const string Attack3 = "Attack3";
    private const string SpecialAttack = "SpecialAttack";
    private const string Speed = "MoveSpeed";
    
    public int IdleHash { get; } = Animator.StringToHash(Idle);
    public int RunHash { get; } = Animator.StringToHash(Run);
    public int DashHash { get; } = Animator.StringToHash(Dash);
    
    public int Attack1Hash { get; } = Animator.StringToHash(Attack1);
    public int Attack2Hash { get; } = Animator.StringToHash(Attack2);
    public int Attack3Hash { get; } = Animator.StringToHash(Attack3);
    
    public int SpecialAttackHash { get; } = Animator.StringToHash(SpecialAttack);
    public int MoveSpeedHash { get; } = Animator.StringToHash(Speed);
}