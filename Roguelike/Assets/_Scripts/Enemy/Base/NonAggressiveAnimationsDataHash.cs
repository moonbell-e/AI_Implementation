using UnityEngine;

public class NonAggressiveAnimationsDataHash
{
    private const string Run = "Running";
    private const string Walk = "Walking";
    private const string Idle = "Idle";
    private const string Die = "Die";
    
    public int RunHash { get; } = Animator.StringToHash(Run);
    public int IdleHash { get; } = Animator.StringToHash(Idle);
    public int WalkHash { get; } = Animator.StringToHash(Walk);
    public int DieHash { get; } = Animator.StringToHash(Die);
}
