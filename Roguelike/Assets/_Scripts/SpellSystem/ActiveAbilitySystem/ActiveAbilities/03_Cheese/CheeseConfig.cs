using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/03_Cheese/SpellConfig", fileName = "CheeseConfig")]
public class CheeseConfig : SpellConfig
{
    [field: SerializeField] public CheeseSceneSpell CheeseSceneSpellPrefab { get; private set; }

    public override SpellBuilder GetBuilder()
    {
        return new CheeseBuilder(this);
    }
}
