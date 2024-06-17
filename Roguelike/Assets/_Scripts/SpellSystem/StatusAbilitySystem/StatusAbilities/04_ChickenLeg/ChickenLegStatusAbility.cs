using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenLegStatusAbility : StatusAbility
{
    private ChickenLegStatusConfig _config;

    private BaseEnemy _enemyObject;

    public ChickenLegStatusAbility(StatusAbilityConfig config)
    {
        _config = (ChickenLegStatusConfig)config;
    }

    public override void Added(BaseEnemy enemyObject)
    {
        _enemyObject = enemyObject;
    }
    public override void Update()
    {
        if (IsActive)
        {
            LifeTime += Time.deltaTime;
            CheckDelayTimer += Time.deltaTime;

            if (CheckDelayTimer >= DelayTime)
            {
                _enemyObject.TakeDamage(DamegeCount);
                CheckDelayTimer = 0.0f;
            }

            if (LifeTime >= ActionTime)
            {
                StopCast();
            }
        }
    }
    public override void StartCast()
    {
        LifeTime = 0.0f;
        CheckDelayTimer = 0.0f;
        IsActive = true;
    }
    public override void StopCast()
    {
        IsActive = false;
    }
}
