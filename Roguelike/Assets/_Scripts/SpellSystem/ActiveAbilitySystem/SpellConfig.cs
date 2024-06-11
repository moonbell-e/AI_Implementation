using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellConfig : ScriptableObject
{
    [field: SerializeField] public int CooldownPoints { get; private set; }

    [field: SerializeField] public float ActionTime { get; private set; }
    [field: SerializeField] public float SpeedCount { get; private set; }
    [field: SerializeField] public float DamegeCount { get; private set; }

    public virtual SpellBuilder GetBuilder() => new SpellBuilder(this);
}
