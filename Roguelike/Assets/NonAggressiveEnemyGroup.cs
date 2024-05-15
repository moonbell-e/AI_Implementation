using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NonAggressiveEnemyGroup : MonoBehaviour
{
    [SerializeField] private List<NonAggressiveEnemy> _enemies;

    private void OnEnable()
    {
        foreach (var enemy in _enemies)
        {
            enemy.OnDamakeTaken += HandleDamageTaken;
        }
    }

    private void OnDisable()
    {
        foreach (var enemy in _enemies)
        {
            enemy.OnDamakeTaken -= HandleDamageTaken;
        }
    }

    private void HandleDamageTaken()
    {
        foreach (var enemy in _enemies)
        {
            enemy.StateMachine.SwitchState(enemy.RunState);
        }
    }
    
    public bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        UnityEngine.AI.NavMeshHit hit;

        if (UnityEngine.AI.NavMesh.SamplePosition(randomPoint, out hit, 1.0f, UnityEngine.AI.NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}