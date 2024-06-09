using UnityEngine;

public class HerbAnimationController : AnimationController
{
    protected override void SetLocomotionClip()
    {
        locomotionClip = Animator.StringToHash("Locomotion");
    }

    protected override void SetAttackClip()
    {
        attackClip = Animator.StringToHash("Attack");
    }

    protected override void SetSpeedHash()
    {
        speedHash = Animator.StringToHash("Speed");
    }
}
