using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/06_Meat/StatusAbilityConfig", fileName = "MeatStatusConfig")]
public class MeatStatusConfig : StatusAbilityConfig
{
    public override StatusAbilityBuilder GetBuilder()
    {
        return new MeatStatusBuilder(this);
    }
}
