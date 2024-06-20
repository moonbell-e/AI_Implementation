using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]

public class Player : MonoBehaviour
{
    [Header("Input Readers")]
    [SerializeField] private InputReaderSwitcher _inputReaderSwitcher;
    [SerializeField] private PlayerInputReader _playerInputReader;
    [SerializeField] private CookingInputReader _cookingInputReader;
    [SerializeField] private InventoryInputReader _inventoryInputReader;

    [Header("Other")]
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private GameObject _knifeGo;
    [SerializeField] private TrailRenderer _trailRenderer;

    private PlayerStateMachine _stateMachine;
    private Transform _mainCamera;
    private Rigidbody _rb;
    public Rigidbody Rb => _rb;
    public PlayerView View => _playerView;
    
    public PlayerConfig Config => _playerConfig;

    public InputReaderSwitcher InputReaderSwitcher => _inputReaderSwitcher;
    public PlayerInputReader PlayerInputReader => _playerInputReader;
    public CookingInputReader CookingInputReader => _cookingInputReader;
    public InventoryInputReader InventoryInputReader => _inventoryInputReader;

    private void Awake()
    {
        _playerView.Initialize(_playerConfig);
        _rb = GetComponent<Rigidbody>();
        _stateMachine = new PlayerStateMachine(this);
        //_trailRenderer.emitting = false;
        _trailRenderer.time = 0;
    }

    private void FixedUpdate()
    {
        _stateMachine.HandleInput();
        _stateMachine.Update();
        if (_trailRenderer.time > 0)
        {
            _trailRenderer.time -= 0.025f;
        }
    }

    public void StartDash()
    {
        _trailRenderer.time = 1;
    }

    public void StartDealDamage()
    {
        var damageDealer = _knifeGo.GetComponentInChildren<DamageDealer>();
        damageDealer.StartDealDamage();
    }
    
    public void EndDealDamage()
    {
        var damageDealer = _knifeGo.GetComponentInChildren<DamageDealer>();
        damageDealer.EndDealDamage();
    }

    public void SetAttackMode(bool value)
    {
        _knifeGo.SetActive(value);
    }
    
    
}
