using System;
using UnityEngine;

public class EnemyDamageDealer : MonoBehaviour
{
    private bool _canDealDamage;
    private bool _hasDealtDamage;

    [SerializeField] private float _weaponLength;
    [SerializeField] private float _weaponDamage;
    [SerializeField] private SensorTypes _sensorType;
    

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
            HitRightEnemy(hit);
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

    private void HitRightEnemy(RaycastHit hit)
    {
        switch (_sensorType)
        {
            case SensorTypes.Robot:
                if (hit.transform.TryGetComponent(out PlayerHealthSystem healthR))
                {
                    healthR.TakeDamage(_weaponDamage);
                    _hasDealtDamage = true;
                }
                else if (hit.transform.TryGetComponent(out NonAggressiveEnemy enemyR))
                {
                    enemyR.TakeDamage(_weaponDamage);
                    _hasDealtDamage = true;
                }
                else if (hit.transform.TryGetComponent(out PredatorGoapAgent predatorR))
                {
                    predatorR.TakeDamage(_weaponDamage);
                    _hasDealtDamage = true;
                }
                else if (hit.transform.TryGetComponent(out PlantEatingGoapAgent plantR))
                {
                    plantR.TakeDamage(_weaponDamage);
                    _hasDealtDamage = true;
                }
                break;
            case SensorTypes.Predator:
                if (hit.transform.TryGetComponent(out PlayerHealthSystem healthP))
                {
                    healthP.TakeDamage(_weaponDamage);
                    _hasDealtDamage = true;
                }
                else if (hit.transform.TryGetComponent(out NonAggressiveEnemy enemyP))
                {
                    enemyP.TakeDamage(_weaponDamage);
                    _hasDealtDamage = true;
                }
                else if (hit.transform.TryGetComponent(out Robot robotP))
                {
                    robotP.TakeDamage(_weaponDamage);
                    _hasDealtDamage = true;
                }
                else if (hit.transform.TryGetComponent(out PlantEatingGoapAgent plantR))
                {
                    plantR.TakeDamage(_weaponDamage);
                    _hasDealtDamage = true;
                }
                break;
            case SensorTypes.Herbal:
                if (hit.transform.TryGetComponent(out PlayerHealthSystem healthPl))
                {
                    healthPl.TakeDamage(_weaponDamage);
                    _hasDealtDamage = true;
                }
                else if (hit.transform.TryGetComponent(out NonAggressiveEnemy enemyP))
                {
                    enemyP.TakeDamage(_weaponDamage);
                    _hasDealtDamage = true;
                }
                else if (hit.transform.TryGetComponent(out Robot robotP))
                {
                    robotP.TakeDamage(_weaponDamage);
                    _hasDealtDamage = true;
                }
                else if (hit.transform.TryGetComponent(out PredatorGoapAgent plantR))
                {
                    plantR.TakeDamage(_weaponDamage);
                    _hasDealtDamage = true;
                }
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        var weaponTransform = transform;
        var position = weaponTransform.position;
        Gizmos.DrawLine(position, position + weaponTransform.forward * _weaponLength);
    }
}