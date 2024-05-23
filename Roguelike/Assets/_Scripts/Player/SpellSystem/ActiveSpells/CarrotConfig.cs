using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Spells/Carrot", fileName = "CarrotConfig")]
public class CarrotConfig : SpellConfig
{
    [field: SerializeField] public float DamageCount { get; private set; }
    [field: SerializeField] public float SpeedCount { get; private set; }
    [field: SerializeField] public float LifeTimeCount { get; private set; }
    [field: SerializeField] public CarrotSceneSpell CarrotSceneSpellPrefab { get; private set; }

    public override SpellBuilder GetBuilder()
    {
        return new CarrotBuilder(this);
    }

}