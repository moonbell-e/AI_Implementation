using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseStatusAbility : StatusAbility
{
    private CheeseStatusConfig _config;

    private GameObject _enemyObject;

    public CheeseStatusAbility(StatusAbilityConfig config)
    {
        _config = (CheeseStatusConfig)config;
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

        GameObject.Instantiate(_config.VFX, _enemyObject.transform.position, _enemyObject.transform.rotation, _enemyObject.transform).Init(ActionTime);
    }
    public override void StopCast()
    {
        IsActive = false;
    }
}
