using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : BaseInputReader
{
    [Header("Action Name References")]
    [SerializeField] private string _dash = "Dash";
    [SerializeField] private string _run = "Run";
    [SerializeField] private string _attack = "Attack";
    [SerializeField] private string _specialAttack = "SpecialAttack";

    private InputAction _runAction;
    private InputAction _dashAction;
    private InputAction _attackAction;
    private InputAction _specialAttackAction;
    
    public Action OnDashTriggered;
    public Action OnAttackTriggered;
    public Action OnSpecialAttackTriggered;
    
    public Vector2 InputDirection { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        
        _runAction = playerInput.FindActionMap(actionMapName).FindAction(_run);
        _dashAction = playerInput.FindActionMap(actionMapName).FindAction(_dash);
        _attackAction = playerInput.FindActionMap(actionMapName).FindAction(_attack);
        _specialAttackAction = playerInput.FindActionMap(actionMapName).FindAction(_specialAttack);
        
        RegisterInputActions();
    }

    private void RegisterInputActions()
    {
        RegisterMovementInputActions();
        
        _attackAction.performed += x => OnAttackTriggered?.Invoke();
        _specialAttackAction.performed += x => OnSpecialAttackTriggered?.Invoke();
    }

    private void RegisterMovementInputActions()
    {
        _runAction.performed += x => InputDirection = x.ReadValue<Vector2>();
        _runAction.canceled += x => InputDirection = Vector2.zero;

        _dashAction.performed += x => OnDashTriggered?.Invoke();
    }

    private void OnEnable()
    {
        _runAction.Enable();
        _dashAction.Enable();
        _attackAction.Enable();
        _specialAttackAction.Enable();
    }

    private void OnDisable()
    {
        _runAction.Disable();
        _dashAction.Disable();
        _attackAction.Disable();
        _specialAttackAction.Disable();
    }
}
