using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/01_Carrot/SpellConfig", fileName = "CarrotConfig")]
public class CarrotConfig : SpellConfig
{
    [field: SerializeField] public CarrotSceneSpell CarrotSceneSpellPrefab { get; private set; }

    public override SpellBuilder GetBuilder()
    {
        return new CarrotBuilder(this);
    }
}