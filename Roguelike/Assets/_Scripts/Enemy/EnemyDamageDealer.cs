using UnityEngine;

public class EnemyDamageDealer : MonoBehaviour
{
    private bool _canDealDamage;
    private bool _hasDealtDamage;

    [SerializeField] private float _weaponLength;
    [SerializeField] private float _weaponDamage;
    [SerializeField] private Vector3 _raycastDirection;

    private void Start()
    {
        _canDealDamage = false;
        _hasDealtDamage = false;
    }

    private void Update()
    {
        if (!_canDealDamage || _hasDealtDamage) return;
        var playerLayerMask = 1 << 6;
        var enemyLayerMask = 1 << 7; 
        var combinedLayerMask = playerLayerMask | enemyLayerMask; 

        if (Physics.Raycast(transform.position, transform.forward, out var hit, _weaponLength, combinedLayerMask))
        {
            if (hit.transform.TryGetComponent(out PlayerHealthSystem health))
            {
                health.TakeDamage(_weaponDamage);
                _hasDealtDamage = true;
            }
            else if (hit.transform.TryGetComponent(out NonAggressiveEnemy enemy))
            {
                enemy.TakeDamage(_weaponDamage);
                _hasDealtDamage = true;
            }
            else if (hit.transform.TryGetComponent(out AggressiveEnemy aggressiveEnemy))
            {
                aggressiveEnemy.TakeDamage(_weaponDamage);
                _hasDealtDamage = true;
            }
        }
    }

    public void StartDealDamage()
    {
        _canDealDamage = true;
        _hasDealtDamage = false;
    }

    public void EndDealDamage()
    {
        _canDealDamage = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        var weaponTransform = transform;
        var position = weaponTransform.position;
        Gizmos.DrawLine(position, position + weaponTransform.forward * _weaponLength);
    }
}