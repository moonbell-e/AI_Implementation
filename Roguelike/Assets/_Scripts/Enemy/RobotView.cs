using UnityEngine;

public class RobotView : MonoBehaviour
{
    private Animator _animator;
    private RobotAnimationDataHash _robotAnimationDataHash;
    
    public void Initialize()
    {
        _animator = GetComponent<Animator>();
        _robotAnimationDataHash = new RobotAnimationDataHash();
    }
    
    public void Run(float velocity, float speed) 
    {
        _animator.SetFloat(_robotAnimationDataHash.SpeedHash, velocity / speed);
        _animator.SetBool(_robotAnimationDataHash.WalkHash, false);
    }

    public void Attack()
    {
        _animator.SetBool(_robotAnimationDataHash.WalkHash, false);
        _animator.SetTrigger(_robotAnimationDataHash.AttackHash);
    }

    public void Walk() => _animator.SetBool(_robotAnimationDataHash.WalkHash, true);
}
