using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private SaveLoadManager _saveLoadManager = null;

    [Header("Other")]
    [SerializeField] private InputReaderSwitcher _inputSwitcher;
    [SerializeField] private CanvasGroup _blackPanel;
    private static readonly int Float = Animator.StringToHash("Float");

    private bool _isStarted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player) && !_isStarted)
        {
            _isStarted = true;
            _inputSwitcher.DisableAllInput();
            player.transform.parent = transform;

            if (_blackPanel != null)
                _blackPanel.DOFade(0.95f, 3f);

            DOVirtual.DelayedCall(3f, LoadLocation);
        }
    }

    private void LoadLocation()
    {
        _saveLoadManager.ClearBigPointOfInterest(PlayerPrefs.GetInt("currenntSave"));
        _saveLoadManager.ClearSmallPointOfInterest(PlayerPrefs.GetInt("currenntSave"));

        _saveLoadManager.AddCurrensy(PlayerPrefs.GetInt("currenntSave"), 0); //�������� �� �������� �����
        _saveLoadManager.SetIsNewSession(PlayerPrefs.GetInt("currenntSave"), false);

        SceneManager.LoadScene("HubLocation");
    }
}
