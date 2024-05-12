using System.Collections.Generic;
using UnityEngine;

public class InputReaderSwitcher : MonoBehaviour
{
    [SerializeField] private List<BaseInputReader> _inputReaders = new();
    private BaseInputReader _activeInputReader;

    public void SetActiveInputReader(BaseInputReader inputReader)
    {
        foreach (var reader in _inputReaders)
        {
            reader.DisableInput();
        }

        _activeInputReader = inputReader;
        _activeInputReader.EnableInput();
    }

    public void DisableAllInput()
    {
        foreach (var reader in _inputReaders)
        {
            reader.DisableInput();
        }
    }

    public void EnableInputReader(BaseInputReader inputReader)
    {
        inputReader.EnableInput();
    }

    public void DisableInputReader(BaseInputReader inputReader)
    {
        inputReader.DisableInput();
    }

    public void CookingModeEnable()
    {
        if (_inputReaders[0] is PlayerInputReader PlayerInputReader)
        {
            PlayerInputReader.CookingModeEnable();
            EnableInputReader(_inputReaders[1]);
        }
    }

    public void CookingModeDisable()
    {
        if (_inputReaders[0] is PlayerInputReader PlayerInputReader)
        {
            PlayerInputReader.CookingModeDisable();
            DisableInputReader(_inputReaders[1]);
        }
    }
}
