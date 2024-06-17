using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/05_Pepper/StatusAbilityConfig", fileName = "PepperStatusConfig")]
public class PepperStatusConfig : StatusAbilityConfig
{
    public override StatusAbilityBuilder GetBuilder()
    {
        return new PepperStatusBuilder(this);
    }
}
