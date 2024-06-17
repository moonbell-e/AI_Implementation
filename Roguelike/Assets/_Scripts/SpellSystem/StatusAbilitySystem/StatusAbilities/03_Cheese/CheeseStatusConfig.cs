using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/03_Cheese/StatusAbilityConfig", fileName = "CheeseStatusConfig")]
public class CheeseStatusConfig : StatusAbilityConfig
{
    public override StatusAbilityBuilder GetBuilder()
    {
        return new CheeseStatusBuilder(this);
    }
}
