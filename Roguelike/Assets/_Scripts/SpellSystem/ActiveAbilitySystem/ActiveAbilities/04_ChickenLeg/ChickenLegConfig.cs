using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/04_ChickenLeg/SpellConfig", fileName = "ChickenLegConfig")]
public class ChickenLegConfig : SpellConfig
{
    [field: SerializeField] public ChickenLegSceneSpell ChickenLegSceneSpellPrefab { get; private set; }

    public override SpellBuilder GetBuilder()
    {
        return new ChickenLegBuilder(this);
    }
}
