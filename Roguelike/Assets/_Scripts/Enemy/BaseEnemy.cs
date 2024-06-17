using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] protected float _health;
    [Header("Combat")] [SerializeField] protected float _attackCooldown = 3f;
    [SerializeField] protected float _attackRange = 1f;
    [SerializeField] protected float _aggroRange = 4f;

    protected Player _player;
    protected float _timePassed;
    protected float _newDestinationCooldown = 0.5f;
    protected NavMeshAgent _agent;
    private Animator _animator;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        _animator.SetFloat("Speed", _agent.velocity.magnitude / _agent.speed);

        if (_timePassed >= _attackCooldown)
        {
            if (Vector3.Distance(transform.position, _player.transform.position) <= _attackRange)
            {
                _animator.SetTrigger("Attack");
                _timePassed = 0;
            }
        }

        _timePassed += Time.deltaTime;

        if (_newDestinationCooldown <= 0 &&
            Vector3.Distance(_player.transform.position, transform.position) <= _aggroRange)
        {
            _agent.SetDestination(_player.transform.position);
            _newDestinationCooldown = 0.5f;
        }

        _newDestinationCooldown -= Time.deltaTime;
        if (_player != null) transform.LookAt(_player.transform);
    }

    public void TakeDamage(float damage)
    {
        _animator.SetTrigger("TakeDamage");
        _health -= damage;

        if (_health <= 0)
        {
            Die();
        }
    }

    public void StartDealDamage()
    {
        GetComponentInChildren<EnemyDamageDealer>().StartDealDamage();
    }

    public void EndDealDamage()
    {
        GetComponentInChildren<EnemyDamageDealer>().EndDealDamage();
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _aggroRange);
    }
}