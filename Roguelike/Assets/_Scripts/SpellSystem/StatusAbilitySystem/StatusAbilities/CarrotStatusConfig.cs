using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/CarrotStatus", fileName = "CarrotStatusConfig")]
public class CarrotStatusConfig : StatusAbilityConfig
{
    public override StatusAbilityBuilder GetBuilder()
    {
        return new CarrotStatusBuilder(this);
    }
}
