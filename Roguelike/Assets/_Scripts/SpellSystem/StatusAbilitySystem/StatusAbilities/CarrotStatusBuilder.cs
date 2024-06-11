using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotStatusBuilder : StatusAbilityBuilder
{
    private readonly CarrotStatusConfig _config;

    public CarrotStatusBuilder(CarrotStatusConfig config) : base(config)
    {
        _config = config;
    }

    public override void Make()
    {
        _statusAbility = new CarrotStatusAbility(_config);

        base.Make();
    }
}
