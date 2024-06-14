using System;
using UnityEngine;

public class MovementStateMachineData
{
    private Vector2 _inputDirection;
    private float _speed;
    private float _dashForce;
    private float _dashDuration;
    private float _dashCooldown;

    private float _currentTargetRotation;
    private float _timeToReachTargetRotation;
    private float _dampedTargetRotationCurrentVelocity;
    private float _dampedTargetRotationPassedTime;
    
    public float LastRotationAngle { get; set; }


    public Vector2 InputDirection
    {
        get => _inputDirection;
        set => _inputDirection = value;
    }

    public float Speed
    {
        get => _speed;

        set
        {
            if (value > 0)
                _speed = value;
            else
                throw new ArgumentOutOfRangeException(nameof(value));
        }
    }

    public float TimeToReachTargetRotation
    {
        get => _timeToReachTargetRotation;
        set
        {
            if (value >= 0)
                _timeToReachTargetRotation = value;
            else
                Debug.LogError($"ArgumentOutOfRangeException: {value}");
        }
    }

    public ref float DampedTargetRotationCurrentVelocity => ref _dampedTargetRotationCurrentVelocity;
    
    public float CurrentTargetRotation
    {
        get => _currentTargetRotation;
        set
        {
            if (value >= 0)
                _currentTargetRotation = value;
        }
    }

    public float DampedTargetRotationPassedTime
    {
        get => _dampedTargetRotationPassedTime;
        set
        {
            if (value >= 0)
                _dampedTargetRotationPassedTime = value;
            else
                Debug.LogError($"ArgumentOutOfRangeException: {value}");
        }
    }
    
    public float DashForce
    {
        get => _dashForce;
        set
        {
            if (value >= 0)
                _dashForce = value;
            else
                Debug.LogError($"ArgumentOutOfRangeException: {value}");
        }
    }
    
    public float DashDuration
    {
        get => _dashDuration;
        set
        {
            if (value >= 0)
                _dashDuration = value;
            else
                Debug.LogError($"ArgumentOutOfRangeException: {value}");
        }
    }
}