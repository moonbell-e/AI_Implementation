using System;
using UnityEngine;

[Serializable]
public class MovementStateConfig
{
    [SerializeField, Range(0, 1)] private float _timeToReachTargetRotation = 0.15f;
    [SerializeField, Range(0, 20)] private float _runningSpeed;
    [SerializeField, Range(0, 5)] private float _runAnimationSpeedModifier;
    [SerializeField] private float _dashForce = 10f;
    [SerializeField] private float _dashDuration = 0.5f;
    [SerializeField] private float _dashCooldown = 1f;

    public float RunningSpeed
    {
        get =>  _runningSpeed;
        set =>  _runningSpeed = value;
    }
    public float RunAnimationSpeedModifier => _runAnimationSpeedModifier;
    public float TimeToReachTargetRotation => _timeToReachTargetRotation;
    
    public float DashForce => _dashForce;

    public float DashDuration => _dashDuration;

    public float DashCooldown => _dashCooldown;
}