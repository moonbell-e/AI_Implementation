using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryInputReader : BaseInputReader
{
    [Header("Action Name References")]
    [SerializeField] private InputActionReference _closeInventory;

    private InputAction _closeInventoryAction;

    private Action<InputAction.CallbackContext> _closeInventoryActionDelegate;

    public Action OnCloseInventoryTriggered;


    protected override void Awake()
    {
        base.Awake();

        _closeInventoryAction = _closeInventory;
    }

    private void RegisterInputActions()
    {
        _closeInventoryActionDelegate = _ => OnCloseInventoryTriggered?.Invoke();
        _closeInventoryAction.performed += _closeInventoryActionDelegate;
    }

    private void UnregisterInputActions()
    {
        _closeInventoryAction.performed -= _closeInventoryActionDelegate;
    }

    private void OnEnable()
    {
        RegisterInputActions();

        _closeInventoryAction.Enable();
    }

    private void OnDisable()
    {
        UnregisterInputActions();

        _closeInventoryAction.Disable();
    }
}
