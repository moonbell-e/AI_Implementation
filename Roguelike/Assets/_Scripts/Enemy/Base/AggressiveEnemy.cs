using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AggressiveEnemy : MonoBehaviour, IDamageable, IEnemyMovable
{
    public EnemyStateMachine StateMachine { get; private set; }
    public AggressiveEnemyIdleState IdleState { get; private set; }
    public AggressiveEnemyChaseState ChaseState { get; private set; }
    public AggressiveEnemyAttackState AttackState { get; private set; }

    private Animator _animator;
    public List<Transform> Targets { get; private set; }

    public NavMeshAgent NavMeshAgent { get; set; }
    [field: SerializeField] public float MaxHealth { get; set; } = 1f;
    [field: SerializeField] public float AttackRange { get; set; } = 1f;
    [field: SerializeField] public float AggroRange { get; set; } = 4f;
    [field: SerializeField] public float AttackCooldown { get; set; } = 2f;
    [field: SerializeField] public float CurrentHealth { get; set; }

    protected virtual void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Targets = new List<Transform>();

        
        _animator = GetComponent<Animator>();

        StateMachine = new EnemyStateMachine();

        IdleState = new AggressiveEnemyIdleState(this);
        ChaseState = new AggressiveEnemyChaseState(this);
        AttackState = new AggressiveEnemyAttackState(this);
    }

    protected virtual void Start()
    {
        CurrentHealth = MaxHealth;
        StateMachine.Initialize(IdleState);
        
        foreach (var target in FindObjectsOfType<RobotTarget>())
        {
            Targets.Add(target.transform);
        }
    }

    private void Update()
    {
        StateMachine.CurrentEnemyState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentEnemyState.FixedUpdate();
    }

    public void TakeDamage(float damageAmount)
    {
        //_animator.SetTrigger("TakeDamage");
        CurrentHealth -= damageAmount;

        if (CurrentHealth <= 0f)
            Die();
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, AggroRange);
    }
}