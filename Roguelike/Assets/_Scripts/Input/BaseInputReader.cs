using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseInputReader: MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] protected InputActionAsset playerInput;

    [Header("Action Map Name References")]
    [SerializeField] protected string actionMapName = "";

    protected virtual void Awake()
    {
        if (actionMapName == "")
            throw new ArgumentException("Action Map Name Not Assigned!");
    }

    public void EnableInput()
    {
        playerInput.FindActionMap(actionMapName).Enable();
    }

    public void DisableInput()
    {
        playerInput.FindActionMap(actionMapName).Disable();
    }
}