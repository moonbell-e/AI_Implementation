using UnityEngine;

public class RobotAnimationDataHash
{
    private const string Speed = "Speed";
    private const string Attack = "Attack";
    private const string Walk = "isWalking";
   
    public int SpeedHash { get; } = Animator.StringToHash(Speed);
    public int AttackHash { get; } = Animator.StringToHash(Attack);
    public int WalkHash { get; } = Animator.StringToHash(Walk);
}
