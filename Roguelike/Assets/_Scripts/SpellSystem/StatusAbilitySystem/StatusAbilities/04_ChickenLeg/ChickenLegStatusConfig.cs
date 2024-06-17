using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/04_ChickenLeg/StatusAbilityConfig", fileName = "ChickenLegStatusConfig")]
public class ChickenLegStatusConfig : StatusAbilityConfig
{
    public override StatusAbilityBuilder GetBuilder()
    {
        return new ChickenLegStatusBuilder(this);
    }
}
