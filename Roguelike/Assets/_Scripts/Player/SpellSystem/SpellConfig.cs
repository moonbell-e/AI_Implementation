using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellConfig : ScriptableObject
{
    [field: SerializeField] public int Id { get; private set; }
    [field: SerializeField] public string Title { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public bool IsOnEnemy { get; private set; }
    [field: SerializeField] public int CooldownPoints { get; private set; }

    public virtual SpellBuilder GetBuilder() => new SpellBuilder(this);
}
