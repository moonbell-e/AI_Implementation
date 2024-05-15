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
        var layerMask = 1 << 6;

        if (Physics.Raycast(transform.position, _raycastDirection, out var hit, _weaponLength, layerMask))
        {
            if (hit.transform.TryGetComponent(out HealthSystem health))
            {
                health.TakeDamage(_weaponDamage);
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
        Gizmos.DrawLine(position, position - _raycastDirection * _weaponLength);
    }
}