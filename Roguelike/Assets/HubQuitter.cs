using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class HubQuitter : MonoBehaviour
{
    [SerializeField] private InputReaderSwitcher _inputSwitcher;
    [SerializeField] private CanvasGroup _blackPanel;
    private Animator _animator;
    private static readonly int Float = Animator.StringToHash("Float");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
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
        SceneManager.LoadScene("Menu");
    }
}