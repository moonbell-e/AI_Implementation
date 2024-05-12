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
    [SerializeField] private InputActionReference _spell;
    [SerializeField] private InputActionReference _interact;
    [SerializeField] private InputActionReference _cooking;
    [SerializeField] private InputActionReference _inventory;
    [SerializeField] private InputActionReference _journal;
    [SerializeField] private InputActionReference _esc;

    private InputAction _runAction;
    private InputAction _dashAction;
    private InputAction _attackAction;
    private InputAction _specialAttackAction;
    private InputAction _spellAction;
    private InputAction _interactAction;
    private InputAction _cookingAction;
    private InputAction _inventoryAction;
    private InputAction _journalAction;
    private InputAction _escAction;

    private Action<InputAction.CallbackContext> _runActionDelegate;
    private Action<InputAction.CallbackContext> _dashActionDelegate;
    private Action<InputAction.CallbackContext> _attackActionDelegate;
    private Action<InputAction.CallbackContext> _specialAttackActionDelegate;
    private Action<InputAction.CallbackContext> _spellActionDelegate;
    private Action<InputAction.CallbackContext> _interactActionDelegate;
    private Action<InputAction.CallbackContext> _cookingActionDelegate;
    private Action<InputAction.CallbackContext> _inventoryActionDelegate;
    private Action<InputAction.CallbackContext> _journalActionDelegate;
    private Action<InputAction.CallbackContext> _escActionDelegate;

    public Action OnDashTriggered;
    public Action OnAttackTriggered;
    public Action OnSpecialAttackTriggered;
    public Action OnSpellTriggered;
    public Action OnInteractTriggered;
    public Action OnInventoryTriggered;
    public Action OnJournalTriggered;
    public Action OnEscTriggered;

    public bool IsCooking { get; private set; }


    public Vector2 InputDirection { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        _runAction = _run;
        _dashAction = _dash;
        _attackAction = _attack;
        _specialAttackAction = _specialAttack;
        _spellAction = _spell;
        _interactAction = _interact;
        _cookingAction = _cooking;
        _inventoryAction = _inventory;
        _journalAction = _journal;
        _escAction = _esc;
    }

    private void RegisterInputActions()
    {
        RegisterMovementInputActions();

        _attackActionDelegate = _ => OnAttackTriggered?.Invoke();
        _attackAction.performed += _attackActionDelegate;

        _specialAttackActionDelegate = _ => OnSpecialAttackTriggered?.Invoke();
        _specialAttackAction.performed += _specialAttackActionDelegate;

        _spellActionDelegate = _ => OnSpellTriggered?.Invoke();
        _spellAction.performed += _spellActionDelegate;

        _interactActionDelegate = _ => OnInteractTriggered?.Invoke();
        _interactAction.performed += _interactActionDelegate;

        _cookingActionDelegate = _ => IsCooking = _.ReadValueAsButton();
        _cookingAction.performed += _cookingActionDelegate;

        _inventoryActionDelegate = _ => OnInventoryTriggered?.Invoke();
        _inventoryAction.performed += _inventoryActionDelegate;

        _journalActionDelegate = _ => OnJournalTriggered?.Invoke();
        _journalAction.performed += _journalActionDelegate;

        _escActionDelegate = _ => OnEscTriggered?.Invoke();
        _escAction.performed += _escActionDelegate;
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
        _spellAction.performed -= _spellActionDelegate;
        _interactAction.performed -= _interactActionDelegate;
        _cookingAction.performed -= _cookingActionDelegate;
        _inventoryAction.performed -= _inventoryActionDelegate;
        _journalAction.performed -= _journalActionDelegate;
        _escAction.performed -= _escActionDelegate;

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
        _spellAction.Enable();
        _interactAction.Enable();
        _cookingAction.Enable();
        _inventoryAction.Enable();
        _journalAction.Enable();
        _escAction.Enable();
    }

    private void OnDisable()
    {
        UnregisterInputActions();

        _runAction.Disable();
        _dashAction.Disable();
        _attackAction.Disable();
        _specialAttackAction.Disable();
        _spellAction.Disable();
        _interactAction.Disable();
        _cookingAction.Disable();
        _inventoryAction.Disable();
        _journalAction.Disable();
        _escAction.Disable();
    }

    public void CookingModeEnable()
    {
        _runAction.Disable();
        _dashAction.Disable();
        _attackAction.Disable();
        _specialAttackAction.Disable();
        _spellAction.Disable();
        _interactAction.Disable();
        _inventoryAction.Disable();
        _journalAction.Disable();
        _escAction.Disable();
    }

    public void CookingModeDisable()
    {
        _runAction.Enable();
        _dashAction.Enable();
        _attackAction.Enable();
        _specialAttackAction.Enable();
        _spellAction.Enable();
        _interactAction.Enable();
        _inventoryAction.Enable();
        _journalAction.Enable();
        _escAction.Enable();
    }
}