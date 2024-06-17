using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/02_Tomato/SpellConfig", fileName = "TomatoConfig")]

public class TomatoConfig : SpellConfig
{
    [field: SerializeField] public TomatoSceneSpell TomatoSceneSpellPrefab { get; private set; }

    public override SpellBuilder GetBuilder()
    {
        return new TomatoBuilder(this);
    }
}
