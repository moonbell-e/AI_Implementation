using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/02_Tomato/StatusAbilityConfig", fileName = "TomatoStatusConfig")]
public class TomatoStatusConfig : StatusAbilityConfig
{
    [field: SerializeField] public float SpeedModofier { get; private set; }

    public override StatusAbilityBuilder GetBuilder()
    {
        return new TomatoStatusBuilder(this);
    }
}
