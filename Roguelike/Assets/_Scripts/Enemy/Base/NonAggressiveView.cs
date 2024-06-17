using UnityEngine;

public class NonAggressiveView : MonoBehaviour
{
    private Animator _animator;
    private NonAggressiveAnimationsDataHash _animationsDataHash;
    private float _animationLength;
    
    public void Initialize()
    {
        _animator = GetComponent<Animator>();
        _animationsDataHash = new NonAggressiveAnimationsDataHash();
    }

    public void StopPlayback()
    {
        _animator.StopPlayback();
    }
    
    public void StartRunning()
    {
        _animator.CrossFade(_animationsDataHash.RunHash, 0f);
    }
        
    public void StartWalking()
    {
        _animator.CrossFade(_animationsDataHash.WalkHash, 0f);
    }
    
    public void StartIdling()
    {
        _animator.CrossFade(_animationsDataHash.IdleHash, 0f);
    }
    
        
    public void Die()
    {
        _animator.CrossFade(_animationsDataHash.DieHash, 0f);
    }
}
