using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoStatusAbility : StatusAbility
{
    private TomatoStatusConfig _config;

    private GameObject _enemyObject;

    public TomatoStatusAbility(StatusAbilityConfig config)
    {
        _config = (TomatoStatusConfig)config;
    }

    public override void Added(GameObject enemyObject)
    {
        _enemyObject = enemyObject;
    }
    public override void Update()
    {
        if (IsActive)
        {
            LifeTime += Time.deltaTime;

            if (LifeTime >= ActionTime)
            {
                StopCast();
            }
        }
    }
    public override void StartCast()
    {
        if (this._enemyObject.TryGetComponent(out IEnemyMovable enemyMovable))
        {
            enemyMovable.NavMeshAgent.speed /= _config.SpeedModofier;
        }

        LifeTime = 0.0f;
        CheckDelayTimer = 0.0f;
        IsActive = true;
    }
    public override void StopCast()
    {
        if (this._enemyObject.TryGetComponent(out IEnemyMovable enemyMovable))
        {
            enemyMovable.NavMeshAgent.speed *= _config.SpeedModofier;
        }
        IsActive = false;
    }
}
