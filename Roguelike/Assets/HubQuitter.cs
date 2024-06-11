using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class HubQuitter : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private SaveLoadManager _saveLoadManager = null;

    [Header("Other")]
    [SerializeField] private InputReaderSwitcher _inputSwitcher;
    [SerializeField] private CanvasGroup _blackPanel;
    private Animator _animator;
    private static readonly int Float = Animator.StringToHash("Float");

    private bool _isStarted = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player) && !_isStarted)
        {
            _isStarted = true;
            _animator.enabled = true;
            GetComponent<Rigidbody>().isKinematic = true;
            _animator.SetTrigger(Float);
            _inputSwitcher.DisableAllInput();
            player.transform.parent = transform;

            if (_blackPanel != null)
                _blackPanel.DOFade(0.95f, 3f);

            DOVirtual.DelayedCall(3f, LoadLocation);
        }
    }

    private void LoadLocation()
    {
        _saveLoadManager.SetCurrensy(PlayerPrefs.GetInt("currenntSave"), 0); //заменить на значение голды
        _saveLoadManager.SetIsNewSession(PlayerPrefs.GetInt("currenntSave"), true);

        SceneManager.LoadScene("Location1");
    }
}