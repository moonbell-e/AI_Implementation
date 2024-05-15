using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    private bool _canDealDamage;
    private List<GameObject> _hasDealtDamage;

    [SerializeField] private float _weaponLength;
    [SerializeField] private int _weaponDamage;
    [SerializeField] private int _criticalDamageMultiplier = 2;
    [SerializeField] private int _criticalDamageChance = 50;
    [SerializeField] private DamagePopupGenerator _damagePopupGenerator;
    [SerializeField] private Vector3 _raycastDirection;


    private void Start()
    {
        _canDealDamage = false;
        _hasDealtDamage = new List<GameObject>();
    }

    private void Update()
    {
        if (!_canDealDamage) return;

        var layerMask = 1 << 7;

        if (Physics.Raycast(transform.position, -transform.forward, out var hit, _weaponLength, layerMask))
        {
            if (hit.transform.TryGetComponent(out AggressiveEnemy enemy) &&
                !_hasDealtDamage.Contains(hit.collider.gameObject))
            {
                int damage = _weaponDamage;
                bool isCritical = Random.value <= _criticalDamageChance / 100f;

                if (isCritical)
                {
                    damage *= _criticalDamageMultiplier;
                }

                enemy.TakeDamage(damage);
                _damagePopupGenerator.Create(enemy.transform.position, damage,
                    isCritical);
                _hasDealtDamage.Add(hit.transform.gameObject);
            }
        }
    }

    public void StartDealDamage()
    {
        _canDealDamage = true;
        _hasDealtDamage.Clear();
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
        Gizmos.DrawLine(position, position - weaponTransform.forward * _weaponLength);
    }
}