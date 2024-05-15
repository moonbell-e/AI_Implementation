using UnityEngine;

public class RobotAnimationDataHash
{
    private const string Idle = "Idle";
    private const string Walk = "Walk";
    private const string Run = "Run";
    private const string Attack = "Attack";
   
    public int IdleHash { get; } = Animator.StringToHash(Idle);
    public int RunHash { get; } = Animator.StringToHash(Run);
    public int WalkHash { get; } = Animator.StringToHash(Walk);
    public int AttackHash { get; } = Animator.StringToHash(Attack);
}
