using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell
{
    public int CooldownPoints { get; private set; }

    public float ActionTime { get; private set; }
    public float SpeedCount { get; private set; }
    public float DamegeCount { get; private set; }


    public int SetCooldownPoints(int cooldownPoints) => CooldownPoints = cooldownPoints;
    public void SetActiveAbility(float actionTime, float speedCount, float damageCount)
    {
        ActionTime = actionTime;
        SpeedCount = speedCount;
        DamegeCount = damageCount;
    }

    public virtual void Added(Transform spellPosition) { }
    public virtual void StartCast(int id1, int id2, int id3) { }
}
