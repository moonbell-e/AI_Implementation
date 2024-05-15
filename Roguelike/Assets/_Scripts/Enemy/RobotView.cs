using System.Collections;
using System.Collections.Generic;
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
    
    public void StartIdling() => _animator.CrossFade(_robotAnimationDataHash.IdleHash, 0.1f);
    public void StartRunning() => _animator.CrossFade(_robotAnimationDataHash.RunHash, 0.1f);
    public void StartWalking() => _animator.CrossFade(_robotAnimationDataHash.WalkHash, 0.1f);
    public void StartAttacking() => _animator.CrossFade(_robotAnimationDataHash.AttackHash, 0.1f);
}
