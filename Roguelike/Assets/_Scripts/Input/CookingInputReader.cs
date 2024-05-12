using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CookingInputReader : BaseInputReader
{
    [Header("Action Name References")]
    [SerializeField] private InputActionReference _ingredient1;
    [SerializeField] private InputActionReference _ingredient2;
    [SerializeField] private InputActionReference _ingredient3;
    [SerializeField] private InputActionReference _ingredient4;
    [SerializeField] private InputActionReference _ingredient5;
    [SerializeField] private InputActionReference _ingredient6;

    private InputAction _ingredient1Action;
    private InputAction _ingredient2Action;
    private InputAction _ingredient3Action;
    private InputAction _ingredient4Action;
    private InputAction _ingredient5Action;
    private InputAction _ingredient6Action;

    private Action<InputAction.CallbackContext> _ingredient1ActionDelegate;
    private Action<InputAction.CallbackContext> _ingredient2ActionDelegate;
    private Action<InputAction.CallbackContext> _ingredient3ActionDelegate;
    private Action<InputAction.CallbackContext> _ingredient4ActionDelegate;
    private Action<InputAction.CallbackContext> _ingredient5ActionDelegate;
    private Action<InputAction.CallbackContext> _ingredient6ActionDelegate;

    public Action OnIngredient1Triggered;
    public Action OnIngredient2Triggered;
    public Action OnIngredient3Triggered;
    public Action OnIngredient4Triggered;
    public Action OnIngredient5Triggered;
    public Action OnIngredient6Triggered;


    protected override void Awake()
    {
        base.Awake();

        _ingredient1Action = _ingredient1;
        _ingredient2Action = _ingredient2;
        _ingredient3Action = _ingredient3;
        _ingredient4Action = _ingredient4;
        _ingredient5Action = _ingredient5;
        _ingredient6Action = _ingredient6;
    }

    private void RegisterInputActions()
    {
        _ingredient1ActionDelegate = _ => OnIngredient1Triggered?.Invoke();
        _ingredient1Action.performed += _ingredient1ActionDelegate;

        _ingredient2ActionDelegate = _ => OnIngredient2Triggered?.Invoke();
        _ingredient2Action.performed += _ingredient2ActionDelegate;

        _ingredient3ActionDelegate = _ => OnIngredient3Triggered?.Invoke();
        _ingredient3Action.performed += _ingredient3ActionDelegate;

        _ingredient4ActionDelegate = _ => OnIngredient4Triggered?.Invoke();
        _ingredient4Action.performed += _ingredient4ActionDelegate;

        _ingredient5ActionDelegate = _ => OnIngredient5Triggered?.Invoke();
        _ingredient5Action.performed += _ingredient5ActionDelegate;

        _ingredient6ActionDelegate = _ => OnIngredient6Triggered?.Invoke();
        _ingredient6Action.performed += _ingredient6ActionDelegate;
    }

    private void UnregisterInputActions()
    {
        _ingredient1Action.performed -= _ingredient1ActionDelegate;
        _ingredient2Action.performed -= _ingredient2ActionDelegate;
        _ingredient3Action.performed -= _ingredient3ActionDelegate;
        _ingredient4Action.performed -= _ingredient4ActionDelegate;
        _ingredient5Action.performed -= _ingredient5ActionDelegate;
        _ingredient6Action.performed -= _ingredient6ActionDelegate;
    }

    private void OnEnable()
    {
        RegisterInputActions();

        _ingredient1Action.Enable();
        _ingredient2Action.Enable();
        _ingredient3Action.Enable();
        _ingredient4Action.Enable();
        _ingredient5Action.Enable();
        _ingredient6Action.Enable();
    }

    private void OnDisable()
    {
        UnregisterInputActions();

        _ingredient1Action.Disable();
        _ingredient2Action.Disable();
        _ingredient3Action.Disable();
        _ingredient4Action.Disable();
        _ingredient5Action.Disable();
        _ingredient6Action.Disable();
    }
}
