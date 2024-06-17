using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class NonAggressiveEnemy : MonoBehaviour, IEnemyMovable, IDamageable
{
    [SerializeField] private NonAggressiveView _nonAggressiveView;
    public NavMeshAgent NavMeshAgent { get; set; }
    public Vector3 Point { get; set; }

    public EnemyStateMachine StateMachine { get; set; }

    public NonAggressiveIdleState IdleState { get; set; }

    public NonAggressiveRunState RunState { get; set; }

    public event Action OnDamakeTaken;

    public NonAggressiveView NonAggressiveView => _nonAggressiveView;
    

    public List<PotentialAttacker> NearByPotentialAttackers = new();

    [field: SerializeField] public float MaxHealth { get; set; }
    [field: SerializeField] public float CurrentHealth { get; set; }
    [SerializeField] private TargetSensor _enemiesSensor;
    [SerializeField] private bool _isGroup;
    
    public bool IsGroup => _isGroup;

    private void OnEnable()
    {
        _enemiesSensor.OnTargetEnter += HandleAgentEnter;
        _enemiesSensor.OnTargetExit += HandleAgentExit;
    }

    private void OnDisable()
    {
        _enemiesSensor.OnTargetEnter -= HandleAgentEnter;
        _enemiesSensor.OnTargetExit -= HandleAgentExit;
    }

    private void HandleAgentEnter(Transform target)
    {
        if (NearByPotentialAttackers.Contains(target.GetComponent<PotentialAttacker>()))
            return;
        
        NearByPotentialAttackers.Add(target.GetComponent<PotentialAttacker>());
        NearByPotentialAttackers.Sort((a, b) => 
        Vector3.SqrMagnitude(b.transform.position - transform.position)
            .CompareTo(Vector3.SqrMagnitude(a.transform.position - transform.position)
            ));
    }

    private void HandleAgentExit(Transform target)
    {
        NearByPotentialAttackers.Remove(target.GetComponent<PotentialAttacker>());
    }


    public void TakeDamage(float damageAmount)
    {
        OnDamakeTaken?.Invoke();
        CurrentHealth -= damageAmount;
        StateMachine.SwitchState(RunState);
        if (CurrentHealth <= 0f)
            Die();
    }

    private void Awake()
    {
        _nonAggressiveView.Initialize();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        StateMachine = new EnemyStateMachine();
        IdleState = new NonAggressiveIdleState(this, StateMachine);
        RunState = new NonAggressiveRunState(this, StateMachine);
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentEnemyState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentEnemyState.FixedUpdate();
    }

    public bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
    
    public void Die()
    {
        StartCoroutine(DieWithAnim());
    }

    private IEnumerator DieWithAnim()
    {
        _nonAggressiveView.Die();
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}