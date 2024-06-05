using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusAbilityConfig : ScriptableObject
{
    [field: SerializeField] public float ActionTime { get; private set; }
    [field: SerializeField] public float DelayTime { get; private set; }
    [field: SerializeField] public float DamegeCount { get; private set; }

    public virtual StatusAbilityBuilder GetBuilder() => new StatusAbilityBuilder(this);
}