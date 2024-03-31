using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private PlayerInputReader _playerInputReader;
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private GameObject _knifeGo;
    [SerializeField] private TrailRenderer _trailRenderer;

    private PlayerStateMachine _stateMachine;
    private Transform _mainCamera;
    private Rigidbody _rb;
    public Rigidbody Rb => _rb;
    public PlayerView View => _playerView;
    
    public PlayerConfig Config => _playerConfig;
    
    public PlayerInputReader InputReader => _playerInputReader;

    private void Awake()
    {
        _playerView.Initialize(_playerConfig);
        _rb = GetComponent<Rigidbody>();
        _stateMachine = new PlayerStateMachine(this);
        _trailRenderer.emitting = false;
    }

    private void Update()
    {
        _stateMachine.HandleInput();
        _stateMachine.Update();
    }

    public void SetDashMode(bool value)
    {
        _trailRenderer.emitting = value;
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
