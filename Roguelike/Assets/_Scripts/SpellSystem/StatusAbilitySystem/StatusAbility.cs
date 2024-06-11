using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusAbility
{
    public float ActionTime { get; private set; }
    public float DelayTime { get; private set; }
    public float DamegeCount { get; private set; }
    public bool IsActive { get; set; } = false;
    public float LifeTime { get; set; } = 0.0f;
    public float CheckDelayTimer { get; set; } = 0.0f;


    public void SetStatusAbility(float actionTime, float delayTime, float damageCount)
    {
        ActionTime = actionTime;
        DelayTime = delayTime;
        DamegeCount = damageCount;
    }

    public virtual void Added(BaseEnemy EnemyObject) { }
    public virtual void StartCast() { }
    public virtual void StopCast() { }
    public virtual void Update() { }
}