using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private PlayerConfig _playerConfig;
    private Animator _animator;
    private PlayerAnimationDataHash _animationsDataHash;
    
    private readonly List<int> _attackHashes = new();
    
    public void Initialize(PlayerConfig playerConfig)
    {
        _animator = GetComponent<Animator>();
        _animationsDataHash = new PlayerAnimationDataHash();
        _playerConfig = playerConfig;
        
        _attackHashes.Add(_animationsDataHash.Attack1Hash);
        _attackHashes.Add(_animationsDataHash.Attack2Hash);
        _attackHashes.Add(_animationsDataHash.Attack3Hash);
    }
    
    public void StartIdling() => _animator.CrossFade(_animationsDataHash.IdleHash, 0.1f);
    public void StartRunning() => _animator.CrossFade(_animationsDataHash.RunHash, 0.1f);
    
    public void StartDashing() => _animator.CrossFade(_animationsDataHash.DashHash, 0f);
    
    public void StartAttacking() => _animator.CrossFade(_attackHashes[Random.Range(0, _attackHashes.Count)], 0.1f);
    
    public void StartSpecialAttacking() => _animator.CrossFade(_animationsDataHash.SpecialAttackHash, 0.1f);
    
    public void StopPlayback() => _animator.StopPlayback();
    
    public void SetRunSpeed(float speedValue) => _animator.SetFloat(_animationsDataHash.MoveSpeedHash, CalculateNewSpeed(speedValue, _playerConfig.MovementConfig.RunAnimationSpeedModifier));

    private float CalculateNewSpeed(float speedValue, float modifier)
    {
        return modifier + (speedValue / 100.0f);
    }
}
