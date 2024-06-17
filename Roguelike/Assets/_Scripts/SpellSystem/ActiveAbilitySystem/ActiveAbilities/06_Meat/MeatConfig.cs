using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/06_Meat/SpellConfig", fileName = "MeatConfig")]
public class MeatConfig : SpellConfig
{
    [field: SerializeField] public MeatSceneSpell MeatSceneSpellPrefab { get; private set; }

    public override SpellBuilder GetBuilder()
    {
        return new MeatBuilder(this);
    }
}
