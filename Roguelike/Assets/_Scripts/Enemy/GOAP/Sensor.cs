using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Sensor : MonoBehaviour
{
    [SerializeField] private SensorTypes _sensorType;
    [SerializeField] private float _detectionRadius = 5f;
    [SerializeField] private float _timerInterval = 1f;

    private SphereCollider _detectionRange;

    public event Action OnTargetChanged = delegate { };
    
    public Vector3 TargetPosition => _target ? _target.transform.position : Vector3.zero;
    public bool IsTargetInRange => TargetPosition != Vector3.zero;


    private GameObject _target;
    private Vector3 _lastKnownPosition;
    private CountdownTimer _timer;
    
    private void Awake()
    {
        _detectionRange = GetComponent<SphereCollider>();
        _detectionRange.isTrigger = true;
        _detectionRange.radius = _detectionRadius;
    }

    private void Start()
    {
        _timer = new CountdownTimer(_timerInterval);
        _timer.OnTimerStop += () =>
        {
            UpdateTargetPosition(_target.OrNull());
            _timer.Start();
        };
        
        _timer.Start();
    }
    
    private void Update()
    {
        _timer.Tick(Time.deltaTime);
    }
    
    private void UpdateTargetPosition(GameObject target = null)
    {
        _target = target;
        
        if (IsTargetInRange && (_lastKnownPosition != TargetPosition || _lastKnownPosition != Vector3.zero))
        {
            _lastKnownPosition = TargetPosition;
            OnTargetChanged.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (_sensorType)
        {
            case SensorTypes.None:
                break;
            case SensorTypes.Robot:
                if (!other.TryGetComponent(out RobotTarget _)) return;
                break;
            case SensorTypes.Predator:
                if (!other.TryGetComponent(out PredatorTarget _)) return;
                break;
            case SensorTypes.Herbal:
                if (!other.TryGetComponent(out PotentialAttacker _)) return;
                break;
        }

        UpdateTargetPosition(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        switch (_sensorType)
        {
            case SensorTypes.None:
                break;
            case SensorTypes.Robot:
                if (!other.TryGetComponent(out RobotTarget _)) return;
                break;
            case SensorTypes.Predator:
                if (!other.TryGetComponent(out PredatorTarget _)) return;
                break;
            case SensorTypes.Herbal:
                if (!other.TryGetComponent(out PotentialAttacker _)) return;
                break;
        }
        
        UpdateTargetPosition();
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = IsTargetInRange ? Color.red : Color.green;
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
    }
}

public enum SensorTypes
{
    None,
    Robot,
    Predator,
    Herbal
}
