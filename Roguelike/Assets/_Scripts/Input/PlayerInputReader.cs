using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : BaseInputReader
{
    [Header("Action Name References")]
    [SerializeField] private InputActionReference _dash;
    [SerializeField] private InputActionReference _run;
    [SerializeField] private InputActionReference _attack;
    [SerializeField] private InputActionReference _specialAttack;

    private InputAction _runAction;
    private InputAction _dashAction;
    private InputAction _attackAction;
    private InputAction _specialAttackAction;

    private Action<InputAction.CallbackContext> _runActionDelegate;
    private Action<InputAction.CallbackContext> _dashActionDelegate;
    private Action<InputAction.CallbackContext> _attackActionDelegate;
    private Action<InputAction.CallbackContext> _specialAttackActionDelegate;


    public Action OnDashTriggered;
    public Action OnAttackTriggered;
    public Action OnSpecialAttackTriggered;

    public Vector2 InputDirection { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        _runAction = _run;
        _dashAction = _dash;
        _attackAction = _attack;
        _specialAttackAction = _specialAttack;
    }

    private void RegisterInputActions()
    {
        RegisterMovementInputActions();

        _attackActionDelegate = _ => OnAttackTriggered?.Invoke();
        _attackAction.performed += _attackActionDelegate;

        _specialAttackActionDelegate = _ => OnSpecialAttackTriggered?.Invoke();
        _specialAttackAction.performed += _specialAttackActionDelegate;
    }

    private void RegisterMovementInputActions()
    {
        _runActionDelegate = x => InputDirection = x.ReadValue<Vector2>();
        _runAction.performed += _runActionDelegate;

        _runActionDelegate = x => InputDirection = Vector2.zero;
        _runAction.canceled += _runActionDelegate;

        _dashActionDelegate = x => OnDashTriggered?.Invoke();
        _dashAction.performed += _dashActionDelegate;
    }

    private void UnregisterInputActions()
    {
        _attackAction.performed -= _attackActionDelegate;
        _specialAttackAction.performed -= _specialAttackActionDelegate;

        UnregisterMovementInputActions();
    }

    private void UnregisterMovementInputActions()
    {
        _runAction.performed -= _runActionDelegate;
        _runAction.canceled -= _runActionDelegate;
        _dashAction.performed -= _dashActionDelegate;
    }

    private void OnEnable()
    {
        RegisterInputActions();

        _runAction.Enable();
        _dashAction.Enable();
        _attackAction.Enable();
        _specialAttackAction.Enable();
    }

    private void OnDisable()
    {
        UnregisterInputActions();

        _runAction.Disable();
        _dashAction.Disable();
        _attackAction.Disable();
        _specialAttackAction.Disable();
    }
}