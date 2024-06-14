using System;
using System.Collections.Generic;
using UnityEngine;

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
}