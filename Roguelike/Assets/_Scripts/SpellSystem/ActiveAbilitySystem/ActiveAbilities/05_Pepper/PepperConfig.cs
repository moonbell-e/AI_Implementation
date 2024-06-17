using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/05_Pepper/SpellConfig", fileName = "PepperConfig")]
public class PepperConfig : SpellConfig
{
    [field: SerializeField] public PepperSceneSpell PepperSceneSpellPrefab { get; private set; }
    [field: SerializeField] public float DelayTime { get; private set; }

    public override SpellBuilder GetBuilder()
    {
        return new PepperBuilder(this);
    }
}
