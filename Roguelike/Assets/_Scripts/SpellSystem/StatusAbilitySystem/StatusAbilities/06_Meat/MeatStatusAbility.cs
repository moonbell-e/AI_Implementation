using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatStatusAbility : StatusAbility
{
    private MeatStatusConfig _config;

    private GameObject _enemyObject;

    public MeatStatusAbility(StatusAbilityConfig config)
    {
        _config = (MeatStatusConfig)config;
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
            CheckDelayTimer += Time.deltaTime;

            if (CheckDelayTimer >= DelayTime)
            {
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
