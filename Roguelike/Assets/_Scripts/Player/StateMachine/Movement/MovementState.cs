using System.Timers;
using UnityEngine;

public class MovementState : IState
{
    protected readonly IStateSwitcher stateSwitcher;
    protected readonly MovementStateMachineData data;

    private readonly Player _player;

    private float _dashCooldownTimer;
    private bool _canDash;

    protected MovementState(IStateSwitcher stateSwitcher, MovementStateMachineData data, Player player)
    {
        this.stateSwitcher = stateSwitcher;
        this.data = data;
        _player = player;

        Initialize();

        _dashCooldownTimer = player.Config.MovementConfig.DashCooldown;
        _canDash = true;
    }

    private void Initialize()
    {
        data.TimeToReachTargetRotation = _player.Config.MovementConfig.TimeToReachTargetRotation;
    }

    protected PlayerView View => _player.View;
    protected PlayerInputReader InputReader => _player.InputReader;
    private Rigidbody Rb => _player.Rb;

    public virtual void Enter()
    {
        AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }

    public virtual void HandleInput()
    {
        data.InputDirection = InputReader.InputDirection;
    }

    public virtual void Update()
    {
        if (IsMovementInputZero())
            Rotate(data.LastRotationAngle);
        else
        {
            var convertedInputDirection = GetConvertedInputDirection();
            var inputAngleDirection = GetDirectionAngle(convertedInputDirection);

            Move(convertedInputDirection);
            Rotate(inputAngleDirection);
        }
        
        _dashCooldownTimer -= Time.deltaTime;

        if (_dashCooldownTimer <= 0)
        {
            _canDash = true;
            _dashCooldownTimer = _player.Config.MovementConfig.DashCooldown;
        }
    }

    private void AddInputActionsCallbacks()
    {
        InputReader.OnDashTriggered += OnDashKeyPressed;
        InputReader.OnAttackTriggered += OnAttackKeyPressed;
        InputReader.OnSpecialAttackTriggered += OnSpecialAttackKeyPressed;
    }

    private void RemoveInputActionsCallbacks()
    {
        InputReader.OnDashTriggered -= OnDashKeyPressed;
        InputReader.OnAttackTriggered -= OnAttackKeyPressed;
        InputReader.OnSpecialAttackTriggered -= OnSpecialAttackKeyPressed;
    }

    protected bool IsMovementInputZero() => data.InputDirection == Vector2.zero;
    private Vector3 GetConvertedInputDirection() => new(data.InputDirection.x, 0, data.InputDirection.y);

    private float GetDirectionAngle(Vector3 inputMoveDirection)
    {
        var directionAngle = Mathf.Atan2(inputMoveDirection.x, inputMoveDirection.z) * Mathf.Rad2Deg;

        if (directionAngle < 0)
            directionAngle += 360;

        return directionAngle;
    }

    private void Rotate(float inputAngleDirection)
    {
        if (inputAngleDirection != data.CurrentTargetRotation)
            UpdateTargetRotationData(inputAngleDirection);

        RotateTowardsTargetRotation();
    }

    private void UpdateTargetRotationData(float targetAngle)
    {
        data.CurrentTargetRotation = targetAngle;
        data.DampedTargetRotationPassedTime = 0f;
    }

    private void RotateTowardsTargetRotation()
    {
        var currentYAngle = GetCurrentRotationAngle();

        if (currentYAngle == data.CurrentTargetRotation)
            return;

        var smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, data.CurrentTargetRotation,
            ref data.DampedTargetRotationCurrentVelocity,
            data.TimeToReachTargetRotation - data.DampedTargetRotationPassedTime);
        data.DampedTargetRotationPassedTime += Time.deltaTime;

        var targetRotation = Quaternion.Euler(0, smoothedYAngle, 0);
        _player.transform.rotation = targetRotation;

        data.LastRotationAngle = smoothedYAngle;
    }

    private float GetCurrentRotationAngle() => _player.transform.rotation.eulerAngles.y;

    private void Move(Vector3 inputMoveDirection)
    {
        var movement = inputMoveDirection * (data.Speed * Time.fixedDeltaTime);
        Rb.MovePosition(Rb.position + movement);
    }

    private void OnDashKeyPressed()
    {
        if (_canDash)
        {
            stateSwitcher.SwitchState<DashingState>();
            _canDash = false;
        }
    }

    private void OnAttackKeyPressed() => stateSwitcher.SwitchState<AttackState>();

    private void OnSpecialAttackKeyPressed() => stateSwitcher.SwitchState<SpecialAttackState>();
}