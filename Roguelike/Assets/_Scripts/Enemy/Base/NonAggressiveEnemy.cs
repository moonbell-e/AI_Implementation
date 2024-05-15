using UnityEngine;
using UnityEngine.AI;

public class NonAggressiveEnemy : MonoBehaviour, IEnemyMovable, IDamageable
{
    public NavMeshAgent NavMeshAgent { get; set; }

    private NonAggressiveStateMachine StateMachine { get; set; }

    private NonAggressiveIdleState IdleState { get; set; }

    private NonAggressiveRunState RunState { get; set; }
    
    [field: SerializeField]public float MaxHealth { get; set; }
    [field: SerializeField]public float CurrentHealth { get; set; }

    public NavMeshAgent PotentialAttacker { get; set; }


    public void TakeDamage(float damageAmount)
    {
        CurrentHealth -= damageAmount;
        StateMachine.SwitchState(RunState);
        if (CurrentHealth <= 0f)
            Die();
    }

    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();

        StateMachine = new NonAggressiveStateMachine();
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
        StateMachine.CurrentNonAggressiveEnemyState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentNonAggressiveEnemyState.FixedUpdate();
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out NavMeshAgent agent))
            PotentialAttacker = agent;
    }
}
